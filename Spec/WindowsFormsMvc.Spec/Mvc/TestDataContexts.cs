﻿// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal static class TestDataContexts
{
    public class TestDataContext;
    public class MultiTestDataContext;
    public class TestDisposableDataContext;

    public interface IAttachingTestDataContext;
    public interface IAttachingTestDataContextFullName;
    public class AttachingTestDataContext;
    public class AttachingTestDataContextFullName;
    public class BaseAttachingTestDataContext;
    public class DerivedBaseAttachingTestDataContext : BaseAttachingTestDataContext;
    public class BaseAttachingTestDataContextFullName;
    public class DerivedBaseAttachingTestDataContextFullName : BaseAttachingTestDataContextFullName;
    public class GenericAttachingTestDataContext<T>;
    public class GenericAttachingTestDataContextFullName<T>;
    public class InterfaceImplementedAttachingTestDataContext : IAttachingTestDataContext;
    public class InterfaceImplementedAttachingTestDataContextFullName : IAttachingTestDataContextFullName;

    public interface ITestContent;
    public class TestContent;
    public class TestContentWithoutDataContextSource;
    public class BaseTestContent;
    public class DerivedTestContent : BaseTestContent;
    public class InterfaceImplementedTestContent : ITestContent;
    public class EmptyContent;
    public class PriorityTestContent;
}