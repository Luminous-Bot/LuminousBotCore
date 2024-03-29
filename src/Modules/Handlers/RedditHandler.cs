﻿

namespace Public_Bot.Modules.Handlers
{
    using System;
    using System.Collections.Generic;

    public partial class RedditHandler
    {
        public string Kind { get; set; }
        public RedditHandlerData Data { get; set; }
    }

    public partial class RedditHandlerData
    {
        public string Modhash { get; set; }
        public long Dist { get; set; }
        public Child[] Children { get; set; }
        public object After { get; set; }
        public object Before { get; set; }
    }

    public partial class Child
    {
        public object Kind { get; set; }
        public ChildData Data { get; set; }
    }

    public partial class ChildData
    {
        public object AuthorFlairBackgroundColor { get; set; }
        public long? ApprovedAtUtc { get; set; }
        public object Subreddit { get; set; }
        public string Selftext { get; set; }
        public string AuthorFullname { get; set; }
        public bool Saved { get; set; }
        public object ModReasonTitle { get; set; }
        public long Gilded { get; set; }
        public bool Clicked { get; set; }
        public string Title { get; set; }
        public object[] LinkFlairRichtext { get; set; }
        public object SubredditNamePrefixed { get; set; }
        public bool Hidden { get; set; }
        public object Pwls { get; set; }
        public object LinkFlairCssClass { get; set; }
        public long Downs { get; set; }
        public long? ThumbnailHeight { get; set; }
        public bool HideScore { get; set; }
        public string Name { get; set; }
        public bool Quarantine { get; set; }
        public object LinkFlairTextColor { get; set; }
        public bool IgnoreReports { get; set; }
        public object SubredditType { get; set; }
        public long Ups { get; set; }
        public long TotalAwardsReceived { get; set; }
        public Gildings MediaEmbed { get; set; }
        public long? ThumbnailWidth { get; set; }
        public object AuthorFlairTemplateId { get; set; }
        public bool IsOriginalContent { get; set; }
        public object[] UserReports { get; set; }
        public object SecureMedia { get; set; }
        public bool IsRedditMediaDomain { get; set; }
        public bool IsMeta { get; set; }
        public object Category { get; set; }
        public Gildings SecureMediaEmbed { get; set; }
        public object LinkFlairText { get; set; }
        public bool CanModPost { get; set; }
        public long Score { get; set; }
        public object ApprovedBy { get; set; }
        public bool AuthorPremium { get; set; }
        public string Thumbnail { get; set; }
        public bool Edited { get; set; }
        public object AuthorFlairCssClass { get; set; }
        public object[] AuthorFlairRichtext { get; set; }
        public Gildings Gildings { get; set; }
        public string PostHint { get; set; }
        public object ContentCategories { get; set; }
        public bool IsSelf { get; set; }
        public object ModNote { get; set; }
        public long Created { get; set; }
        public object LinkFlairType { get; set; }
        public object Wls { get; set; }
        public object RemovedByCategory { get; set; }
        public object BannedBy { get; set; }
        public object AuthorFlairType { get; set; }
        public object Domain { get; set; }
        public bool AllowLiveComments { get; set; }
        public string SelftextHtml { get; set; }
        public bool? Likes { get; set; }
        public object SuggestedSort { get; set; }
        public object BannedAtUtc { get; set; }
        public object ViewCount { get; set; }
        public bool Archived { get; set; }
        public bool NoFollow { get; set; }
        public bool Spam { get; set; }
        public bool IsCrosspostable { get; set; }
        public bool Pinned { get; set; }
        public bool Over18 { get; set; }
        public Preview Preview { get; set; }
        public object[] AllAwardings { get; set; }
        public object[] Awarders { get; set; }
        public bool MediaOnly { get; set; }
        public bool CanGild { get; set; }
        public bool Removed { get; set; }
        public bool Spoiler { get; set; }
        public bool Locked { get; set; }
        public object AuthorFlairText { get; set; }
        public bool Visited { get; set; }
        public object RemovedBy { get; set; }
        public long NumReports { get; set; }
        public object Distinguished { get; set; }
        public object SubredditId { get; set; }
        public object ModReasonBy { get; set; }
        public object RemovalReason { get; set; }
        public string LinkFlairBackgroundColor { get; set; }
        public string Id { get; set; }
        public bool IsRobotIndexable { get; set; }
        public object[] ReportReasons { get; set; }
        public string Author { get; set; }
        public object DiscussionType { get; set; }
        public long NumComments { get; set; }
        public bool SendReplies { get; set; }
        public object WhitelistStatus { get; set; }
        public bool ContestMode { get; set; }
        public object[] ModReports { get; set; }
        public bool AuthorPatreonFlair { get; set; }
        public bool Approved { get; set; }
        public object AuthorFlairTextColor { get; set; }
        public string Permalink { get; set; }
        public object ParentWhitelistStatus { get; set; }
        public bool Stickied { get; set; }
        public Uri Url { get; set; }
        public long SubredditSubscribers { get; set; }
        public long CreatedUtc { get; set; }
        public long NumCrossposts { get; set; }
        public object Media { get; set; }
        public bool IsVideo { get; set; }
        public Dictionary<string, MediaMetadatum> MediaMetadata { get; set; }
        public string RteMode { get; set; }
    }

    public partial class Gildings
    {
    }

    public partial class MediaMetadatum
    {
        public string Status { get; set; }
        public string E { get; set; }
        public string M { get; set; }
        public S[] P { get; set; }
        public S S { get; set; }
        public string Id { get; set; }
    }

    public partial class S
    {
        public long Y { get; set; }
        public long X { get; set; }
        public Uri U { get; set; }
    }

    public partial class Preview
    {
        public RedditImage[] Images { get; set; }
        public bool Enabled { get; set; }
    }

    public partial class RedditImage
    {
        public Source Source { get; set; }
        public Source[] Resolutions { get; set; }
        public Gildings Variants { get; set; }
        public string Id { get; set; }
    }

    public partial class Source
    {
        public Uri Url { get; set; }
        public long Width { get; set; }
        public long Height { get; set; }
    }
}
