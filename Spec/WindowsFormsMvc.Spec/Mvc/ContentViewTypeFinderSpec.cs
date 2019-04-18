// Copyright (C) 2018-2019 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;
using Carna;

namespace Charites.Windows.Mvc
{
    [Specification("ContentViewTypeFinder Spec")]
    class ContentViewTypeFinderSpec : FixtureSteppable
    {
        IContentViewTypeFinder Finder { get; } = new ContentViewTypeFinder();

        [Example("Finds the type of the view that is associated with the specified content")]
        [Sample(Source = typeof(ContentViewTypeSampleDataSource))]
        void Ex01(object content, Type expectedViewType)
        {
            Expect("the type of the view that is associated with the content should be found", () => Finder.Find(content) == expectedViewType);
        }

        class ContentViewTypeSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new
                {
                    Description = "When the content view is associated with the content type",
                    Content = new TestDataContexts.TestContent(),
                    ExpectedViewType = typeof(TestControls.TestContentView)
                };
                yield return new
                {
                    Description = "When the content view is associated with the content base type",
                    Content = new TestDataContexts.DerivedTestContent(),
                    ExpectedViewType = typeof(TestControls.TestContentViewSpecifiedByBaseType)
                };
                yield return new
                {
                    Description = "When the content view is associated with the interface type",
                    Content = new TestDataContexts.InterfaceImplementedTestContent(),
                    ExpectedViewType = typeof(TestControls.TestContentViewSpecifiedByInterface)
                };
                yield return new
                {
                    Description = "When the content view is associated with the content type and priority is high",
                    Content = new TestDataContexts.PriorityTestContent(),
                    ExpectedViewType = typeof(TestControls.TestPriorityHighContentView)
                };
            }
        }
    }
}
