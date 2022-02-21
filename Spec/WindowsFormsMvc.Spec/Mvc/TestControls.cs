// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal static class TestControls
{
    public class TestControl : UserControl
    {
        public object? DataContext
        {
            get => DataContextSource.Value;
            set => DataContextSource.Value = value;
        }
        protected readonly DataContextSource DataContextSource = new();

        public void RaiseHandleCreated() => OnHandleCreated(EventArgs.Empty);
        public void RaiseLoad() => OnLoad(EventArgs.Empty);
        public void RaiseClick() => OnClick(EventArgs.Empty);
    }

    public class SingleControllerView : TestControl { }
    public class MultiControllerView : TestControl { }

    public class AttachingTestControl : TestControl { }

    [ContentView(typeof(TestDataContexts.TestContent))]
    public class TestContentView : TestControl { }

    [ContentView(typeof(TestDataContexts.BaseTestContent))]
    public class TestContentViewSpecifiedByBaseType : TestControl { }

    [ContentView(typeof(TestDataContexts.ITestContent))]
    public class TestContentViewSpecifiedByInterface : TestControl { }

    [ContentView(typeof(TestDataContexts.PriorityTestContent))]
    public class TestPriorityLowContentView : TestControl { }

    [ContentView(typeof(TestDataContexts.PriorityTestContent), Priority = 1)]
    public class TestPriorityHighContentView : TestControl { }
}