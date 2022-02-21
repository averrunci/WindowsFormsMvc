// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.ComponentModel;
using Charites.Windows.Mvc;

namespace Charites.Windows.Forms;

/// <summary>
/// Represents a control that has a content.
/// </summary>
public class ContentControl : Control
{
    /// <summary>
    /// Gets or sets a default finder to find a type of a view that is associated with a content.
    /// </summary>
    public static IContentViewTypeFinder DefaultContentViewTypeFinder { get; set; } = new ContentViewTypeFinder();

    /// <summary>
    /// Gets or sets a default finder to find a data context defined in a view.
    /// </summary>
    public static IWindowsFormsDataContextFinder DefaultDataContextFinder { get; set; } = new WindowsFormsDataContextFinder();

    /// <summary>
    /// Gets or sets a type of a view that is associated with a content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Type? ViewType
    {
        get => viewType;
        set
        {
            if (viewType == value) return;

            viewType = value;
            if (content is not null) InitializeComponent();
        }
    }
    private Type? viewType;

    /// <summary>
    /// Gets or sets a content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object? Content
    {
        get => content;
        set
        {
            if (content == value) return;

            content = value;
            InitializeComponent();
        }
    }
    private object? content;

    /// <summary>
    /// Gets or sets a finder to find a type of a view that is associated with a content.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IContentViewTypeFinder ContentViewTypeFinder { get; set; } = DefaultContentViewTypeFinder;

    /// <summary>
    /// Gets or sets a finder to find a data context defined in a view.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IWindowsFormsDataContextFinder DataContextFinder { get; set; } = DefaultDataContextFinder;

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the user can give the focus
    /// to this control using the TAB key.
    /// </summary>
    [DefaultValue(false)]
    public new bool TabStop
    {
        get => base.TabStop;
        set => base.TabStop = value;
    }

    private void InitializeComponent()
    {
        Controls.OfType<Control>().ForEach(control => control.Dispose());
        Controls.Clear();

        var contentViewType = FindContentViewType();
        if (contentViewType is null)
        {
            CreateDefaultContentView();
            return;
        }

        if (Activator.CreateInstance(contentViewType) is not Control contentView) return;

        contentView.SetDataContext(Content, DataContextFinder);
        contentView.Dock = DockStyle.Fill;
        Controls.Add(contentView);
    }

    /// <summary>
    /// Finds the type of the view that is associated with the content.
    /// </summary>
    /// <returns>Tye type of the view that is associated with the content.</returns>
    protected virtual Type? FindContentViewType() => ViewType ?? (Content is not null ? ContentViewTypeFinder.Find(Content) : null);

    /// <summary>
    /// Creates a default view that is associated with the content.
    /// </summary>
    protected virtual void CreateDefaultContentView()
    {
        if (Content is null) return;

        Controls.Add(new Label { Dock = DockStyle.Fill, Text = Content.ToString() });
    }
}