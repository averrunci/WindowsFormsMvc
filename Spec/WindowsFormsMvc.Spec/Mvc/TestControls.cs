// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;

namespace Charites.Windows.Mvc;

internal static class TestControls
{
    public class TestComponent : Component
    {
        public event EventHandler? Updated;

        public void RaiseUpdated() => Updated?.Invoke(this, EventArgs.Empty);
    }

    public class TestControl : UserControl
    {
        public TestComponent? GetTestComponent() => testComponent;
        private readonly TestComponent? testComponent = new();

        public object? DataContext
        {
            get => DataContextSource.Value;
            set => DataContextSource.Value = value;
        }
        protected readonly DataContextSource DataContextSource = new();

        public void RaiseHandleCreated() => OnHandleCreated(EventArgs.Empty);
        public void RaiseLoad() => OnLoad(EventArgs.Empty);
        public void RaiseClick() => OnClick(EventArgs.Empty);
        public void RaiseTestComponentUpdated() => testComponent?.RaiseUpdated();
    }

    public class SingleControllerView : TestControl;
    public class MultiControllerView : TestControl;

    public class AttachingTestControl : TestControl;

    [ContentView(typeof(TestDataContexts.TestContent))]
    public class TestContentView : TestControl;

    [ContentView(typeof(TestDataContexts.BaseTestContent))]
    public class TestContentViewSpecifiedByBaseType : TestControl;

    [ContentView(typeof(TestDataContexts.ITestContent))]
    public class TestContentViewSpecifiedByInterface : TestControl;

    [ContentView(typeof(TestDataContexts.PriorityTestContent))]
    public class TestPriorityLowContentView : TestControl;

    [ContentView(typeof(TestDataContexts.PriorityTestContent), Priority = 1)]
    public class TestPriorityHighContentView : TestControl;
}