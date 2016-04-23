// <copyright file="StartPage.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Pages
{
    using EPiCore.Content.Models.Attributes;
    using EPiCore.Content.Models.Misc;
    using EPiCore.Content.Models.Pages;
    using EPiCore.Models.Attributes;
    using EPiCore.Models.Pages;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    /// <summary>
    /// The <see cref="StartPage" /> class.
    /// </summary>
    [ContentType(
        GUID = "85334249-488f-4fa9-9843-8baa07475283",
        GroupName = GroupNames.Content,
        Order = 100)]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[] { typeof(BasePage) },
        Exclude = new[] { typeof(StartPage), typeof(SettingsPage), typeof(ErrorPage) })]
    [IncludeOnRoot]
    [IncludeOnAToZ(false)]
    public class StartPage : EPiCore.Content.Models.Pages.StartPage
    {
    }
}