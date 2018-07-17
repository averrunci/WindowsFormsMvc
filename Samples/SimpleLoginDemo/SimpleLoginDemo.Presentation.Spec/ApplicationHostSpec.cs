// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [Specification("ApplicationHost Spec")]
    class ApplicationHostSpec : FixtureSteppable
    {
        ApplicationHost ApplicationHost { get; }
        ILoginDemoContent InitialContent { get; }= Substitute.For<ILoginDemoContent>();
        ILoginDemoContent NextContent { get; }= Substitute.For<ILoginDemoContent>();

        public ApplicationHostSpec()
        {
            ApplicationHost = new ApplicationHost(InitialContent);
        }

        [Example("When an instance is created")]
        void Ex01()
        {
            Expect("an initial content specified in a constructor should be set", () => ApplicationHost.Content.Value == InitialContent);
        }

        [Example("Changes a content")]
        void Ex02()
        {
            When("the content raises the ContentRequested event", () => ApplicationHost.Content.Value.ContentRequested += Raise.EventWith(InitialContent, new ContentRequestedEventArgs(NextContent)));
            Then("the requested content should be set", () => ApplicationHost.Content.Value == NextContent);

            When("the initial content raises the ContentRequested event again", () => InitialContent.ContentRequested += Raise.EventWith(InitialContent, new ContentRequestedEventArgs(Substitute.For<ILoginDemoContent>())));
            Then("the content should not be changed (the requested content should not be set)", () => ApplicationHost.Content.Value == NextContent);
        }
    }
}
