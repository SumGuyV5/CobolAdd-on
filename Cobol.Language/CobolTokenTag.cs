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

namespace RichardAllen.Cobol.Language
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;
    using RichardAllen.Cobol.Language.Classification; //for CobolClassifierProvider.ContentType

    [Export(typeof(ITaggerProvider))]
    [ContentType(CobolClassifierProvider.ContentType)]
    [TagType(typeof(CobolTokenTag))]
    internal sealed class CobolTokenTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new CobolTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class CobolTokenTag : ITag
    {
        public CobolTokenTypes type { get; private set; }

        public CobolTokenTag(CobolTokenTypes type)
        {
            this.type = type;
        }
    }

    internal sealed class CobolTokenTagger : ITagger<CobolTokenTag>
    {
        ITextBuffer _buffer;
        IDictionary<string, CobolTokenTypes> _cobolTypes;

        internal CobolTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _cobolTypes = new Dictionary<string, CobolTokenTypes>();
       
            foreach ( var key in Words.keywordlst )
            {
                _cobolTypes[key] = CobolTokenTypes.keywords;
            }

            foreach ( var key in Words.divisionlst )
            {
                _cobolTypes[key] = CobolTokenTypes.divisions;
            }

            foreach (var key in Words.commentlst)
            {
                _cobolTypes[key] = CobolTokenTypes.comments;
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<CobolTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;
                string[] tokens = containingLine.GetText().ToLower().Split(new char[] {' ', '.'});

                for (int count = 0; count < tokens.Length; count++ )
                {
                    bool com = false;
                    
                    string cobolToken = tokens[count];

                    string cobolToken2 = cobolToken;
                    if (count + 1 < tokens.Length)
                        cobolToken2 += " " + tokens[count + 1];

                    if (count == 0)
                    {
                        foreach (string comment in Words.commentlst)
                        {
                            if ((cobolToken.Contains(comment)) && (cobolToken.IndexOfAny(comment.ToCharArray()) < 7))
                            {
                                cobolToken = comment;
                                com = true;
                            }
                        }
                    }

                    if (_cobolTypes.ContainsKey(cobolToken2))
                        cobolToken = cobolToken2;

                    if (_cobolTypes.ContainsKey(cobolToken))
                    {
                        SnapshotSpan tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, cobolToken.Length));

                        if (com)    //Line is commented out so lets turn it all green.
                            tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(containingLine.Start, containingLine.GetText().Length));

                        if (((cobolToken != Words.commentlst[0]) && (com == false)) || ((cobolToken == Words.commentlst[0]) && (com)))
                        {
                            if (tokenSpan.IntersectsWith(curSpan))
                                yield return new TagSpan<CobolTokenTag>(tokenSpan,
                                                                      new CobolTokenTag(_cobolTypes[cobolToken]));
                        }
                    }
                    if (com)    //We know the line is commented out so there is no need to check the other tokens in the span
                        break;
                    //add an extra char location because of the space
                    curLoc += cobolToken.Length + 1;
                }
            }
        }
    }
}
