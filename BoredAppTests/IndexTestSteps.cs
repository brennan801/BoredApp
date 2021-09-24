using System;
using TechTalk.SpecFlow;
using BoredWebApp.Pages;
using FluentAssertions;
using BoredWebApp.Services;
using BoredShared.Models;
using System.Threading.Tasks;

namespace BoredAppTests
{
    [Binding]
    public class IndexTestSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IBoredAPIService boredAPIService;

        public IndexTestSteps(ScenarioContext scenarioContext, IBoredAPIService boredAPIService)
        {
            this.scenarioContext = scenarioContext;
            this.boredAPIService = boredAPIService;
        }

        [Given(@"the page is index")]
        public void GivenThePageIsIndex()
        {
            var pageModel = new IndexModel(boredAPIService);
            scenarioContext.Add("pageModel", pageModel);
        }


        [Given(@"the index button is clicked")]
        public async Task GivenTheIndexButtonIsClicked()
        {
            var pageModel = scenarioContext.Get<IndexModel>("pageModel");
            await pageModel.OnPost();
            scenarioContext.Add("activity", pageModel.Activity);
        }

        [Then(@"the activity should be a valid activity")]
        public void ThenTheActivityShouldBeAValidResponse()
        {
            var activity = scenarioContext.Get<ActivityModel>("activity");
            activity.Activity.Should().NotBeNull();
        }

    }
}
