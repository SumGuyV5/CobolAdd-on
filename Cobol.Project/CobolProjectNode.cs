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
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Project;
using System.Drawing;
using System.Windows.Forms;

namespace RichardAllen.Cobol.Project
{
    class CobolProjectNode : ProjectNode
    {
        private CobolProjectPackage package;

        private static ImageList imageList;

        internal static int imageIndex;

        static CobolProjectNode()
        {
            imageList = Utilities.GetImageList(typeof(CobolProjectNode).Assembly.GetManifestResourceStream("RichardAllen.Cobol.Project.Resources.CobolProjectNode.bmp"));
        }

        public CobolProjectNode(CobolProjectPackage package)
        {
            this.package = package;

            imageIndex = this.ImageHandler.ImageList.Images.Count;

            foreach (Image img in imageList.Images)
            {
                this.ImageHandler.AddImage(img);
            }
        }
        public override Guid ProjectGuid
        {
            get { return GuidList.guidCobolProjectFactory; }
        }
        public override string ProjectType
        {
            get { return "CobolProjectType"; }
        }

        /*public override void AddFileFromTemplate(string source, string target)
        {
            string nameSpace = this.FileTemplateProcessor.GetFileNamespace(target, this);
            string className = Path.GetFileNameWithoutExtension(target);

            this.FileTemplateProcessor.AddReplace("$programname$", className);

            this.FileTemplateProcessor.UntokenFile(source, target);
            this.FileTemplateProcessor.Reset();
        }*/

        

        public override int ImageIndex
        {
            get { return imageIndex; }
        }
    }
}
