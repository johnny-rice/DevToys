﻿namespace DevToys.Blazor.Components;

public partial class Button : JSStyledComponentBase
{
    [Parameter]
    public ButtonAppearance Appearance { get; set; } = ButtonAppearance.Neutral;

    /// <summary>
    /// Gets or sets the content to be rendered inside the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void OnParametersSet()
    {
        CSS.Clear();
        CSS.Add($"type-{Appearance.Class}");

        if (IsActuallyEnabled && AdditionalAttributes is not null)
        {
            AdditionalAttributes.Remove("disabled");
        }

        if (!IsActuallyEnabled)
        {
            CSS.Add("disabled");

            AdditionalAttributes ??= new Dictionary<string, object>();

            AdditionalAttributes.TryAdd("disabled", true);
        }

        base.OnParametersSet();
    }

    internal ValueTask<bool> FocusAsync()
    {
        return JSRuntime.InvokeVoidWithErrorHandlingAsync("devtoys.DOM.setFocus", Element);
    }
}
