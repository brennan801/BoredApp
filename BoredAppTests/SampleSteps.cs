using BoredWebApp.Services;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace BoredAppTests
{
    [Binding]
    public class SampleSteps
    {
        private readonly ScenarioContext context;

        public SampleSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int a)
        {
            context.Add("first number", a);
        }
        
        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int b)
        {
            context.Add("second number", b);
        }
        
        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            var a = context.Get<int>("first number");
            var b = context.Get<int>("second number");
            var sum = Calculator.Add(a, b);
            context.Add("sum", sum);
        }

        [When(@"the two numbers are multiplied")]
        public void WhenTheTwoNumbersAreMultiplied()
        {
            var a = context.Get<int>("first number");
            var b = context.Get<int>("second number");
            var product = Calculator.Multiply(a, b);
            context.Add("product", product);
        }


        [Then(@"the sum should be (.*)")]
        public void ThenTheResultShouldBe(int expectedSum)
        {
            var actualSum = context.Get<int>("sum");

            actualSum.Should().Be(expectedSum);
        }

        [Then(@"the product should be (.*)")]
        public void ThenTheProductShouldBe(int expectedProduct)
        {
            var actualProduct = context.Get<int>("product");
            actualProduct.Should().Be(expectedProduct);
        }

    }
}
