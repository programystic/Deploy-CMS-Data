//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.0.90
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;



namespace Umbraco.Web.PublishedContentModels
{
	// Mixin content Type 1178 with alias "pageMetaData"
	/// <summary>Page Meta Data</summary>
	public partial interface IPageMetaData : IPublishedContent
	{
	}

	/// <summary>Page Meta Data</summary>
	[PublishedContentModel("pageMetaData")]
	public partial class PageMetaData : PublishedContentModel, IPageMetaData
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "pageMetaData";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public PageMetaData(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<PageMetaData, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}
	}

	// Mixin content Type 1179 with alias "contentBase"
	/// <summary>Content Base</summary>
	public partial interface IContentBase : IPublishedContent
	{
		/// <summary>Content</summary>
		IHtmlString BodyText { get; }

		/// <summary>Page Title</summary>
		string PageTitle { get; }
	}

	/// <summary>Content Base</summary>
	[PublishedContentModel("contentBase")]
	public partial class ContentBase : PublishedContentModel, IContentBase
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "contentBase";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public ContentBase(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<ContentBase, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Content
		///</summary>
		[ImplementPropertyType("bodyText")]
		public IHtmlString BodyText
		{
			get { return GetBodyText(this); }
		}

		/// <summary>Static getter for Content</summary>
		public static IHtmlString GetBodyText(IContentBase that) { return that.GetPropertyValue<IHtmlString>("bodyText"); }

		///<summary>
		/// Page Title: The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters
		///</summary>
		[ImplementPropertyType("pageTitle")]
		public string PageTitle
		{
			get { return GetPageTitle(this); }
		}

		/// <summary>Static getter for Page Title</summary>
		public static string GetPageTitle(IContentBase that) { return that.GetPropertyValue<string>("pageTitle"); }
	}

	// Mixin content Type 1180 with alias "navigationBase"
	/// <summary>Navigation Base</summary>
	public partial interface INavigationBase : IPublishedContent
	{
		/// <summary>Keywords</summary>
		object Keywords { get; }

		/// <summary>Description</summary>
		string SeoMetaDescription { get; }

		/// <summary>Hide in Navigation</summary>
		bool UmbracoNavihide { get; }
	}

	/// <summary>Navigation Base</summary>
	[PublishedContentModel("navigationBase")]
	public partial class NavigationBase : PublishedContentModel, INavigationBase
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "navigationBase";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public NavigationBase(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<NavigationBase, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Keywords: Keywords that describe the content of the page. This is consired optional since most modern search engines don't use this anymore
		///</summary>
		[ImplementPropertyType("keywords")]
		public object Keywords
		{
			get { return GetKeywords(this); }
		}

		/// <summary>Static getter for Keywords</summary>
		public static object GetKeywords(INavigationBase that) { return that.GetPropertyValue("keywords"); }

		///<summary>
		/// Description: A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130
		///</summary>
		[ImplementPropertyType("seoMetaDescription")]
		public string SeoMetaDescription
		{
			get { return GetSeoMetaDescription(this); }
		}

		/// <summary>Static getter for Description</summary>
		public static string GetSeoMetaDescription(INavigationBase that) { return that.GetPropertyValue<string>("seoMetaDescription"); }

		///<summary>
		/// Hide in Navigation: If you don't want this page to appear in the navigation, check this box
		///</summary>
		[ImplementPropertyType("umbracoNavihide")]
		public bool UmbracoNavihide
		{
			get { return GetUmbracoNavihide(this); }
		}

		/// <summary>Static getter for Hide in Navigation</summary>
		public static bool GetUmbracoNavihide(INavigationBase that) { return that.GetPropertyValue<bool>("umbracoNavihide"); }
	}

	/// <summary>Home Page</summary>
	[PublishedContentModel("homePage")]
	public partial class HomePage : PublishedContentModel, IContentBase, INavigationBase, IPageMetaData
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "homePage";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public HomePage(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<HomePage, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Main Content
		///</summary>
		[ImplementPropertyType("mainContent")]
		public Newtonsoft.Json.Linq.JToken MainContent
		{
			get { return this.GetPropertyValue<Newtonsoft.Json.Linq.JToken>("mainContent"); }
		}

		///<summary>
		/// Content
		///</summary>
		[ImplementPropertyType("bodyText")]
		public IHtmlString BodyText
		{
			get { return ContentBase.GetBodyText(this); }
		}

		///<summary>
		/// Page Title: The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters
		///</summary>
		[ImplementPropertyType("pageTitle")]
		public string PageTitle
		{
			get { return ContentBase.GetPageTitle(this); }
		}

		///<summary>
		/// Keywords: Keywords that describe the content of the page. This is consired optional since most modern search engines don't use this anymore
		///</summary>
		[ImplementPropertyType("keywords")]
		public object Keywords
		{
			get { return NavigationBase.GetKeywords(this); }
		}

		///<summary>
		/// Description: A brief description of the content on your page. This text is shown below the title in a google search result and also used for Social Sharing Cards. The ideal length is between 130
		///</summary>
		[ImplementPropertyType("seoMetaDescription")]
		public string SeoMetaDescription
		{
			get { return NavigationBase.GetSeoMetaDescription(this); }
		}

		///<summary>
		/// Hide in Navigation: If you don't want this page to appear in the navigation, check this box
		///</summary>
		[ImplementPropertyType("umbracoNavihide")]
		public bool UmbracoNavihide
		{
			get { return NavigationBase.GetUmbracoNavihide(this); }
		}
	}

	/// <summary>Website</summary>
	[PublishedContentModel("websiteRoot")]
	public partial class WebsiteRoot : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "websiteRoot";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public WebsiteRoot(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<WebsiteRoot, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}
	}

	/// <summary>All Data Types</summary>
	[PublishedContentModel("allDataTypes")]
	public partial class AllDataTypes : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "allDataTypes";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public AllDataTypes(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<AllDataTypes, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Check Box
		///</summary>
		[ImplementPropertyType("checkBox")]
		public bool CheckBox
		{
			get { return this.GetPropertyValue<bool>("checkBox"); }
		}

		///<summary>
		/// Content Picker
		///</summary>
		[ImplementPropertyType("contentPicker")]
		public object ContentPicker
		{
			get { return this.GetPropertyValue("contentPicker"); }
		}

		///<summary>
		/// Date Picker
		///</summary>
		[ImplementPropertyType("datePicker")]
		public DateTime DatePicker
		{
			get { return this.GetPropertyValue<DateTime>("datePicker"); }
		}

		///<summary>
		/// Drop Down
		///</summary>
		[ImplementPropertyType("dropDown")]
		public object DropDown
		{
			get { return this.GetPropertyValue("dropDown"); }
		}

		///<summary>
		/// Drop Down Multiple
		///</summary>
		[ImplementPropertyType("dropDownMultiple")]
		public object DropDownMultiple
		{
			get { return this.GetPropertyValue("dropDownMultiple"); }
		}

		///<summary>
		/// Image Cropper
		///</summary>
		[ImplementPropertyType("imageCropper")]
		public Umbraco.Web.Models.ImageCropDataSet ImageCropper
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("imageCropper"); }
		}

		///<summary>
		/// Label
		///</summary>
		[ImplementPropertyType("label")]
		public object Label
		{
			get { return this.GetPropertyValue("label"); }
		}

		///<summary>
		/// List View Content
		///</summary>
		[ImplementPropertyType("listViewContent")]
		public object ListViewContent
		{
			get { return this.GetPropertyValue("listViewContent"); }
		}

		///<summary>
		/// List View Media
		///</summary>
		[ImplementPropertyType("listViewMedia")]
		public object ListViewMedia
		{
			get { return this.GetPropertyValue("listViewMedia"); }
		}

		///<summary>
		/// List View Members
		///</summary>
		[ImplementPropertyType("listViewMembers")]
		public object ListViewMembers
		{
			get { return this.GetPropertyValue("listViewMembers"); }
		}

		///<summary>
		/// Media Picker
		///</summary>
		[ImplementPropertyType("mediaPicker")]
		public object MediaPicker
		{
			get { return this.GetPropertyValue("mediaPicker"); }
		}

		///<summary>
		/// Member Picker
		///</summary>
		[ImplementPropertyType("memberPicker")]
		public object MemberPicker
		{
			get { return this.GetPropertyValue("memberPicker"); }
		}

		///<summary>
		/// Multiple Media Picker
		///</summary>
		[ImplementPropertyType("multipleMediaPicker")]
		public string MultipleMediaPicker
		{
			get { return this.GetPropertyValue<string>("multipleMediaPicker"); }
		}

		///<summary>
		/// Numeric
		///</summary>
		[ImplementPropertyType("numeric")]
		public int Numeric
		{
			get { return this.GetPropertyValue<int>("numeric"); }
		}

		///<summary>
		/// Radio Box
		///</summary>
		[ImplementPropertyType("radioBox")]
		public object RadioBox
		{
			get { return this.GetPropertyValue("radioBox"); }
		}

		///<summary>
		/// Related Links
		///</summary>
		[ImplementPropertyType("relatedLinks")]
		public Newtonsoft.Json.Linq.JArray RelatedLinks
		{
			get { return this.GetPropertyValue<Newtonsoft.Json.Linq.JArray>("relatedLinks"); }
		}

		///<summary>
		/// Rich Text Editor
		///</summary>
		[ImplementPropertyType("richTextEditor")]
		public IHtmlString RichTextEditor
		{
			get { return this.GetPropertyValue<IHtmlString>("richTextEditor"); }
		}

		///<summary>
		/// Tags
		///</summary>
		[ImplementPropertyType("tags")]
		public object Tags
		{
			get { return this.GetPropertyValue("tags"); }
		}

		///<summary>
		/// Text Area
		///</summary>
		[ImplementPropertyType("textArea")]
		public object TextArea
		{
			get { return this.GetPropertyValue("textArea"); }
		}

		///<summary>
		/// Text String
		///</summary>
		[ImplementPropertyType("textString")]
		public string TextString
		{
			get { return this.GetPropertyValue<string>("textString"); }
		}

		///<summary>
		/// Upload
		///</summary>
		[ImplementPropertyType("upload")]
		public object Upload
		{
			get { return this.GetPropertyValue("upload"); }
		}
	}

	/// <summary>Folder</summary>
	[PublishedContentModel("Folder")]
	public partial class Folder : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Folder";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Folder(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Folder, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Contents:
		///</summary>
		[ImplementPropertyType("contents")]
		public object Contents
		{
			get { return this.GetPropertyValue("contents"); }
		}
	}

	/// <summary>Image</summary>
	[PublishedContentModel("Image")]
	public partial class Image : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Image";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public Image(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Image, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public object UmbracoBytes
		{
			get { return this.GetPropertyValue("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public object UmbracoExtension
		{
			get { return this.GetPropertyValue("umbracoExtension"); }
		}

		///<summary>
		/// Upload image
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public Umbraco.Web.Models.ImageCropDataSet UmbracoFile
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.ImageCropDataSet>("umbracoFile"); }
		}

		///<summary>
		/// Height
		///</summary>
		[ImplementPropertyType("umbracoHeight")]
		public object UmbracoHeight
		{
			get { return this.GetPropertyValue("umbracoHeight"); }
		}

		///<summary>
		/// Width
		///</summary>
		[ImplementPropertyType("umbracoWidth")]
		public object UmbracoWidth
		{
			get { return this.GetPropertyValue("umbracoWidth"); }
		}
	}

	/// <summary>File</summary>
	[PublishedContentModel("File")]
	public partial class File : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "File";
		public new const PublishedItemType ModelItemType = PublishedItemType.Media;
#pragma warning restore 0109

		public File(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<File, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Size
		///</summary>
		[ImplementPropertyType("umbracoBytes")]
		public object UmbracoBytes
		{
			get { return this.GetPropertyValue("umbracoBytes"); }
		}

		///<summary>
		/// Type
		///</summary>
		[ImplementPropertyType("umbracoExtension")]
		public object UmbracoExtension
		{
			get { return this.GetPropertyValue("umbracoExtension"); }
		}

		///<summary>
		/// Upload file
		///</summary>
		[ImplementPropertyType("umbracoFile")]
		public object UmbracoFile
		{
			get { return this.GetPropertyValue("umbracoFile"); }
		}
	}

	/// <summary>Member</summary>
	[PublishedContentModel("Member")]
	public partial class Member : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "Member";
		public new const PublishedItemType ModelItemType = PublishedItemType.Member;
#pragma warning restore 0109

		public Member(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<Member, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Is Approved
		///</summary>
		[ImplementPropertyType("umbracoMemberApproved")]
		public bool UmbracoMemberApproved
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberApproved"); }
		}

		///<summary>
		/// Comments
		///</summary>
		[ImplementPropertyType("umbracoMemberComments")]
		public string UmbracoMemberComments
		{
			get { return this.GetPropertyValue<string>("umbracoMemberComments"); }
		}

		///<summary>
		/// Failed Password Attempts
		///</summary>
		[ImplementPropertyType("umbracoMemberFailedPasswordAttempts")]
		public object UmbracoMemberFailedPasswordAttempts
		{
			get { return this.GetPropertyValue("umbracoMemberFailedPasswordAttempts"); }
		}

		///<summary>
		/// Last Lockout Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLockoutDate")]
		public object UmbracoMemberLastLockoutDate
		{
			get { return this.GetPropertyValue("umbracoMemberLastLockoutDate"); }
		}

		///<summary>
		/// Last Login Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastLogin")]
		public object UmbracoMemberLastLogin
		{
			get { return this.GetPropertyValue("umbracoMemberLastLogin"); }
		}

		///<summary>
		/// Last Password Change Date
		///</summary>
		[ImplementPropertyType("umbracoMemberLastPasswordChangeDate")]
		public object UmbracoMemberLastPasswordChangeDate
		{
			get { return this.GetPropertyValue("umbracoMemberLastPasswordChangeDate"); }
		}

		///<summary>
		/// Is Locked Out
		///</summary>
		[ImplementPropertyType("umbracoMemberLockedOut")]
		public bool UmbracoMemberLockedOut
		{
			get { return this.GetPropertyValue<bool>("umbracoMemberLockedOut"); }
		}

		///<summary>
		/// Password Answer
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalAnswer")]
		public object UmbracoMemberPasswordRetrievalAnswer
		{
			get { return this.GetPropertyValue("umbracoMemberPasswordRetrievalAnswer"); }
		}

		///<summary>
		/// Password Question
		///</summary>
		[ImplementPropertyType("umbracoMemberPasswordRetrievalQuestion")]
		public object UmbracoMemberPasswordRetrievalQuestion
		{
			get { return this.GetPropertyValue("umbracoMemberPasswordRetrievalQuestion"); }
		}
	}

}
