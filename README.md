# Deploy CMS Data

Deploy CMS Data is a component that you can use for your Umbraco application which allows you to build migrations that deploy CMS updates.

The updates are run on startup, and the status is saved to the database so that once they are run successfully, they are never run again.

---

Version|Umbraco Version|Status
--- | --- | ---
7.6.0.3 | 7.6.0|Published to Nuget
8.0.0 | 8.0.0|Work in progress
7.13.0 | 7.13.0|Work in progress
7.4.0 | 7.4.0|Work in progress
7.0.0 | 7.0.0|Work in progress

---

## Creating an Upgrade script

### Install from Nuget

```PM> Install-Package DeployCmsData.UmbracoCms -Version 7.6.0.3```

Create a new class that implements IUpgradeScript

```csharp
using DeployCmsData.Core.Interfaces;

public class Script01 : IUpgradeScript
{
    public bool RunScript(IUpgradeLogRepository upgradeLog)
    {
        return true;
    }
}
```
## Naming of upgrade scripts
The scripts are run on startup in name order. So you will need to make sure you name them correctly so that they are run in the correct order.

The easiest way is to use the same name with a numeric value postfix. 

Example: ```Script0010.cs, Script00020.cs, Script00010.cs ...```

---

### RunScriptEveryTime Attributes

The script is only ever run once, so during development you might want to add the following attribute so that the script is run every time on startup, even if it's already been run.

```csharp
[RunScriptEveryTime]
public class Script01 : IUpgradeScript
```

--- 

## DeployCmsData Builders

Within our upgrade script we can use builders to manipulate the CMS data.

## Document Type Folder Builder

To create new folders to structure your document types:
```csharp
new DocumentTypeFolderBuilder("Compositions")
    .BuildAtRoot();

new DocumentTypeFolderBuilder("Composition Sub-Section")
    .BuildWithParentFolder("Compositions");
```

## Document Type Builder

The basic command to create a new document type is:
```csharp
new DocumentTypeBuilder("contentBase")
    .BuildAtRoot();
```

To specify a name, icon and folder:
```csharp
new DocumentTypeBuilder("pageMetaData")
                .Name("Page Meta Data")
                .Icon(Icons.MindMap)
                .BuildInFolder("Compositions");
```

To add fields to the document type:
```csharp
 var builder = new DocumentTypeBuilder("contentBase");

builder
    .Name("Content Base")
    .Icon(Icons.Document);

builder.AddField("pageTitle")
    .Description("The title of the page, this is also the first text in a google search result. The ideal length is between 40 and 60 characters")
    .IsMandatory()
    .Tab("Content");

builder.AddField("bodyText")
    .Name("Content")
    .DataType(DataType.RichTextEditor)
    .Tab("Content");

builder.BuildInFolder("Compositions");
```

To add existing document types as compositions to the document type:
```csharp
var builder = new DocumentTypeBuilder("homePage");

builder
    .Name("Home Page")
    .Icon(Icons.Home)
    .AddComposition("pageMetaData")
    .AddComposition("contentBase")
    .AddComposition("navigationBase");

builder.AddField("mainContent")
    .DataType(LocalDataTypes.Grid)
    .Tab("Content");

builder.BuildInFolder("Pages");
```