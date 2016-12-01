using System;
using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.Presentation.Views
{
    public interface IConnectionsSettingsView : ISettingsView
    {
        string RecipesServiceAddress { get; set; }
        string RemainsServiceAddress { get; set; }

        string RecipesFolderPath { get; set; }
        string RemainsFolderPath { get; set; }
    }
}
