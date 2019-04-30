/************************************************************************
**  Program Name:   Cobol Add-on			      		                **
**  Version Number: V0.6                                     		    **
**  Copyright (C):  September 20, 2011  Richard W. Allen	   	        **
**  Date Started:   September 1, 2011	                      		    **
**  Date Ended:     September 20, 2011    	             		        **
**  Author:         Richard W. Allen                                    **
**  Webpage:        http://www.richardallenonline.com			        **
**  IDE:            Visual Studio 2010 SP1				                **
**  Compiler:	    C# 4.0				      		                    **
**  Langage:        C#							                        **
**  License:	    GNU GENERAL PUBLIC LICENSE Version 2		        **
**		            see license.txt for for details	      		        **
*************************************************************************/

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace RichardAllen.Cobol.Language
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the OrdinaryClassification type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "keywords")]
    [Name("keywords")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class Keywords : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public Keywords()
        {
            this.DisplayName = "keywords"; //human readable version of the name
            this.ForegroundColor = Colors.Blue;
        }
    }

    /// <summary>
    /// Defines an editor format for the OrdinaryClassification type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "divisions")]
    [Name("divisions")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class Divisions : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public Divisions()
        {
            this.DisplayName = "divisions"; //human readable version of the name
            this.ForegroundColor = Colors.Maroon;
        }
    }

    /// <summary>
    /// Defines an editor format for the OrdinaryClassification type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "comments")]
    [Name("comments")]
    //this should be visible to the end user
    [UserVisible(false)]
    //set the priority to be after the default classifiers
    [Order(Before = Priority.Default)]
    internal sealed class Comments : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "ordinary" classification type
        /// </summary>
        public Comments()
        {
            this.DisplayName = "comments"; //human readable version of the name
            this.ForegroundColor = new Color { A = 255, R = 0, B = 0, G = 128 };
        }
    }

    #endregion //Format definition
}
