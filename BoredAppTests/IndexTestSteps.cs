using System;
using TechTalk.SpecFlow;
using BoredWebApp.Pages;
using FluentAssertions;
using BoredWebApp.Services;
using BoredShared.Models;
using System.Threading.Tasks;
using Moq;

namespace BoredAppTests
{
    [Binding]
    public class IndexTestSteps
    {
        private readonly ScenarioContext scenarioContext;

        public IndexTestSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I make a generic call")]
        public void GivenIMakeAGenericCall()
        {
            var boredServiceMock = new Mock<IBoredAPIService>();
            boredServiceMock.Setup(m => m.GetRandomActivity()).Returns(
                Task.FromResult(
                    new ActivityModel
                    {
                        Activity = "bogus activity",
                        Accessibility = .5
                    }));
            var pageModel = new IndexModel(boredServiceMock.Object);
            scenarioContext.Add("pageModel", pageModel);
        }


        [When(@"the index button is clicked")]
        public async Task GivenTheIndexButtonIsClicked()
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            await pageModel.OnGet();
        }

        [Then(@"the activity should be a valid activity")]
        public void ThenTheActivityShouldBeAValidResponse()
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var activity = pageModel.Activity;
            activity.Activity.Should().Be("bogus activity");
            activity.Accessibility.Should().Be(.5);
        }

        [Given(@"I make a specified call")]
        public void GivenIMakeASpecifiedCall()
        {
            var boredServiceMock = new Mock<IBoredAPIService>();
            var pageModel = new IndexModel(boredServiceMock.Object);
            pageModel.ActivityFormRequest = new ActivityFormRequest();
            ActivityModel responce = new ActivityModel();
            scenarioContext.Add("pageModel", pageModel);
            scenarioContext.Add("boredServiceMock", boredServiceMock);
            scenarioContext.Add("responce", responce);
        }

        [Given(@"the activity type is: (.*)")]
        public void GivenTheActivityTypeIs___(string type)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var responce = scenarioContext.Get<ActivityModel>("responce");
            pageModel.ActivityFormRequest.Type = type;
            responce.Type = type;
            scenarioContext.Set<IndexModel>(pageModel, "pageModel");
            /*scenarioContext.Remove("pageModel");
            scenarioContext.Add("pageModel", pageModel);*/
            scenarioContext.Remove("responce");
            scenarioContext.Add("responce", responce);
        }

        [Given(@"the number of participants is: (.*)")]
        public void GivenTheNumberOfParticipantsIs___(int participants)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var responce = scenarioContext.Get<ActivityModel>("responce");
            pageModel.ActivityFormRequest.Participants = participants;
            responce.Participants = participants;
            scenarioContext.Remove("pageModel");
            scenarioContext.Add("pageModel", pageModel);
            scenarioContext.Remove("responce");
            scenarioContext.Add("responce", responce);
        }

        [Given(@"the price is: (.*)")]
        public void GivenThePriceIs___(string price)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var responce = scenarioContext.Get<ActivityModel>("responce");
            pageModel.ActivityFormRequest.Price = price;
            var minAndMaxPrice = pageModel.computeMinAndMaxPrice(price);
            var minPrice = minAndMaxPrice[0];
            var maxPrice = minAndMaxPrice[1];
            responce.Price = (minPrice + maxPrice) / 2;
            scenarioContext.Remove("pageModel");
            scenarioContext.Add("pageModel", pageModel);
            scenarioContext.Remove("responce");
            scenarioContext.Add("responce", responce);
        }

        [When(@"the specifed button is clicked")]
        public async Task WhenTheSpecifedButtonIsClicked()
        {
            var responce = scenarioContext.Get<ActivityModel>("responce");
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var boredServiceMock = scenarioContext.Get<Mock<IBoredAPIService>>("boredServiceMock");
            boredServiceMock.Setup(m => m.GetSpecificActivity(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<double>(),
                It.IsAny<double>()))
                .Returns(Task.FromResult(responce));
            await pageModel.OnPost();
        }

        [Then(@"I should get a response with type of (.*), participants of (.*), minPrice of (.*), and maxPrice of (.*)")]
        public void ThenIShouldGetAResponseWithTypeOf________ParticipantsOf______MinPriceOf_______AndMaxPriceOf___
            (string expectedType,
            int expectedParticipants, 
            double expectedMinPrice,
            double expectedMaxPrice)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            pageModel.SpecificActivity.Type.Should().Be(expectedType);
            pageModel.SpecificActivity.Participants.Should().Be(expectedParticipants);
            pageModel.SpecificActivity.Price.Should().BeGreaterOrEqualTo(expectedMinPrice);
            pageModel.SpecificActivity.Price.Should().BeLessOrEqualTo(expectedMaxPrice);
        }


        [Given(@"The selected price is (.*)")]
        public void GivenTheSelectedPriceIs___(string price)
        {
            var boredServiceMock = new Mock<IBoredAPIService>();
            var pageModel = new IndexModel(boredServiceMock.Object);
            scenarioContext.Add("pageModel", pageModel);
            scenarioContext.Add("price", price);
        }

        [When(@"the minPrice and maxPrice are computed")]
        public void WhenTheMinPriceAndMaxPriceAreComputed()
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var price = scenarioContext.Get<string>("price");
            double[] actualMinAndMaxPrice = pageModel.computeMinAndMaxPrice(price);
            scenarioContext.Add("actualMinPrice", actualMinAndMaxPrice[0]);
            scenarioContext.Add("actualMaxPrice", actualMinAndMaxPrice[1]);
        }

        [Then(@"the minPrice and maxPrice should be (.*), and (.*)")]
        public void ThenTheMinPriceAndMaxPriceShouldBe_(double expectedMin, double expectedMax)
        {
            var actualMinPrice = scenarioContext.Get<double>("actualMinPrice");
            var actualMaxPrice = scenarioContext.Get<double>("actualMaxPrice");
            actualMinPrice.Should().Be(expectedMin);
            actualMaxPrice.Should().Be(expectedMax);
        }

        [Given(@"the user makes a specified call where the activity doesn't exist")]
        public void GivenTheUserMakesASpecifiedCallWhereTheActivityDoesntExist()
        {
            var errorActivity = new ActivityModel();
            errorActivity.Error = "Endpoint not found";
            var mockService = new Mock<IBoredAPIService>();
            mockService.Setup(m => m.GetSpecificActivity(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<double>(),
                It.IsAny<double>()))
                .Returns(Task.FromResult(errorActivity));
            var pageModel = new IndexModel(mockService.Object);
            pageModel.ActivityFormRequest.Price = "low";
            scenarioContext.Add("pageModel", pageModel);
        }

        [When(@"the call is made")]
        public async Task WhenTheCallIsMade()
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            await pageModel.OnPost();
            scenarioContext.Remove("pageModel");
            scenarioContext.Add("pageModel", pageModel);
        }

        [Then(@"the returned activity name should be ""(.*)""")]
        public void ThenTheReturnedActivityNameShouldBe(string expectedName)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            var actualName = pageModel.SpecificActivity.Activity;
            actualName.Should().Be(expectedName);
        }

    }
}
