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
            var specificActivity = new ActivityModel();
            scenarioContext.Add("specificActivity", specificActivity);
        }

        [Given(@"the activity type is: (.*)")]
        public void GivenTheActivityTypeIs___(string type)
        {
            var specificActivity = scenarioContext.Get<ActivityModel>("specificActivity");
            specificActivity.Type = type;
            scenarioContext.Remove("specificActivity");
            scenarioContext.Add("specificActivity", specificActivity);
        }

        [Given(@"the number of participants is: (.*)")]
        public void GivenTheNumberOfParticipantsIs___(string participantsString)
        {
            int? participants;
            if (participantsString == "null")
            {
                participants = null;
            }
            else
            {
                participants = int.Parse(participantsString);
            }
            var specificActivity = scenarioContext.Get<ActivityModel>("specificActivity");
            specificActivity.Participants = participants;
            scenarioContext.Remove("specificActivity");
            scenarioContext.Add("specificActivity", specificActivity);
        }

        [Given(@"the price is: (.*)")]
        public void GivenThePriceIs___(string priceString)
        {
            double? price;
            if (priceString == "null")
            {
                price = null;
            }
            else price = double.Parse(priceString);
            var specificActivity = scenarioContext.Get<ActivityModel>("specificActivity");
            specificActivity.Price = price;
            scenarioContext.Remove("specificActivity");
            scenarioContext.Add("specificActivity", specificActivity);
        }

        [When(@"the specifed button is clicked")]
        public async Task WhenTheSpecifedButtonIsClicked()
        {
            var specificActivity = scenarioContext.Get<ActivityModel>("specificActivity");
            var boredServiceMock = new Mock<IBoredAPIService>();
            boredServiceMock.Setup(m => m.GetSpecificActivity(specificActivity.Type,
                specificActivity.Participants,
                specificActivity.Price)).Returns(
                    Task.FromResult(specificActivity));
            var pageModel = new IndexModel(boredServiceMock.Object);
            scenarioContext.Add("pageModel", pageModel);
            await pageModel.OnPost();
        }

        [Then(@"I should send a request with: (.*) , (.*), and (.*)")]
        public void ThenIShouldSendARequestWith____________And____(string sentType, int sentParticipants, double sentPrice)
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            pageModel.Activity.Type.Should().Be(sentType);
            pageModel.Activity.Participants.Should().Be(sentParticipants);
            pageModel.Activity.Price.Should().Be(sentParticipants);
        }


    }
}
