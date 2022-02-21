// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using NSubstitute;

namespace Charites.Windows.Mvc;

[Specification("DataContextSource Spec")]
class DataContextSourceSpec : FixtureSteppable
{
    DataContextSource DataContextSource { get; } = new();

    DataContextChangedEventHandler Handler { get; } = Substitute.For<DataContextChangedEventHandler>();

    object DataContext { get; } = new();
    object AnotherDataContext { get; } = new();

    public DataContextSourceSpec()
    {
        DataContextSource.DataContextChanged += Handler;
    }

    [Example("Occurs the DataContextChanged event when the data context value is changed")]
    void Ex01()
    {
        When("a data context is set to the DataContextSource", () => DataContextSource.Value = DataContext);
        Then("the DataContextChanged event should be raised", () => Handler.Received().Invoke(DataContextSource, Arg.Is<DataContextChangedEventArgs>(e => e.OldValue == null && e.NewValue == DataContext)));

        Handler.ClearReceivedCalls();
        When("another data context is set to the DataContextSource", () => DataContextSource.Value = AnotherDataContext);
        Then("the DataContextChanged event should be raised", () => Handler.Received().Invoke(DataContextSource, Arg.Is<DataContextChangedEventArgs>(e => e.OldValue == DataContext && e.NewValue == AnotherDataContext)));

        Handler.ClearReceivedCalls();
        When("the same data context is set to the DataContextSource", () => DataContextSource.Value = AnotherDataContext);
        Then("the DataContextChanged event should not be raised", () => Handler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<DataContextChangedEventArgs>()));

        Handler.ClearReceivedCalls();
        When("null is set to the DataContextSource", () => DataContextSource.Value = null);
        Then("the DataContextChanged event should be raised", () => Handler.Received().Invoke(DataContextSource, Arg.Is<DataContextChangedEventArgs>(e => e.OldValue == AnotherDataContext && e.NewValue == null)));
    }

    [Example("Sets null to the Value property when the DataContextSource is disposed")]
    void Ex02()
    {
        When("a data context is set to the DataContextSource", () => DataContextSource.Value = DataContext);

        Handler.ClearReceivedCalls();
        When("the DataContextSource is disposed", () => DataContextSource.Dispose());
        Then("the value of the DataContextSource should be null", () => DataContextSource.Value == null);
        Then("the DataContextChanged event should be raised", () => Handler.Received().Invoke(DataContextSource, Arg.Is<DataContextChangedEventArgs>(e => e.OldValue == DataContext && e.NewValue == null)));
    }
}