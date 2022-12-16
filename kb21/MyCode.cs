using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Windows.Documents; 
using kb_lib;
using static kb_lib.Log;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Forms;

namespace kb12
{
    class MyCode : TextEditor, MyCtrl
    {
        KbWindow win;
        string id;
        string last_under_caret;
        public MyCode(MyArg arg, KbWindow _win)
        {
            id = arg.Get("id");
            win = _win;

            FontSize = 24;
            ShowLineNumbers = true;
            Background = Brushes.Black;
            Foreground= Brushes.White;
            // Load our custom highlighting definition
            IHighlightingDefinition customHighlighting;
            ColorizeSearchResultsBackgroundRenderer searchColorizor = new();
            using (Stream s = typeof(MyFrame).Assembly.GetManifestResourceStream("kb12.lua.xshd"))
            {
                if (s == null)
                    ok("Could not find embedded resource");
                else
                    using (XmlReader reader = new XmlTextReader(s))
                    {
                        customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                        HighlightingManager.Instance.RegisterHighlighting("Lua", new string[] { ".lua" }, customHighlighting);
                    }

            }
            SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("Lua");



            TextArea.TextView.BackgroundRenderers.Add(searchColorizor);





            TextArea.Caret.PositionChanged += (sender, args) =>
            {
                //win.Integration("caret",id);
                var tmp = GetCurrentWord();
                if(last_under_caret!= tmp)
                {
                    last_under_caret = tmp;
                    foreach (var markSameWord in TextArea.TextView.LineTransformers.OfType<MarkSameWord>().ToList())
                    {
                        TextArea.TextView.LineTransformers.Remove(markSameWord);
                    }
                    if(!string.IsNullOrWhiteSpace(last_under_caret))
                    {
                        TextArea.TextView.LineTransformers.Add(new MarkSameWord(last_under_caret));
                    }
                }
                


            };

            AvalonEditCommands.DeleteLine.InputGestures.Clear();

            RoutedCommand newCmd = new("ctrlD", typeof(string));
            newCmd.InputGestures.Add(new KeyGesture(Key.D,ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd, MyCtrlD));


        }
        void MyCtrlD(object sender, ExecutedRoutedEventArgs e)
        {
            int offset = CaretOffset;
            DocumentLine line = Document.GetLineByOffset(offset);
            string tmp=Document.GetText(line.Offset, line.Length);
            int end=line.EndOffset;            
            Document.Insert(end, System.Environment.NewLine + tmp);

        }

        string GetCurrentWord()
        {
            int offset = CaretOffset;
            int offsetStart = TextUtilities.GetNextCaretPosition(Document, offset, LogicalDirection.Backward, CaretPositioningMode.WordBorder);
            int offsetEnd = TextUtilities.GetNextCaretPosition(Document, offset, LogicalDirection.Forward, CaretPositioningMode.WordBorder);

            if (offsetEnd == -1 || offsetStart == -1)
                return string.Empty;

            var currentChar = Document.GetText(offset, 1);

            if (string.IsNullOrWhiteSpace(currentChar))
                return string.Empty;
            string a = Document.GetText(offsetStart, offsetEnd - offsetStart);
            string b = new string((from c in a
                              where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)||c=='_'
                              select c).ToArray());
            return b;

        }




        public bool Cmd(MyArg arg)
        {
            if (arg.Is("set"))
            {
                Text = arg.Get("set");
                return false;
            }

            if (arg.Is("get"))
                return arg.Set("text", Text);
            
            if (arg.Try("insert",out string text))
            {                
                int caretOffset = CaretOffset;
                Document.Insert(caretOffset, text);
                return false;
            }

            if (arg.Is("current_word"))
                return arg.Set("text", GetCurrentWord());

            if (arg.Is("caret"))
                return arg.Set("caret",CaretOffset);

            if (arg.Try("caret_set", out int val))
            {
                if (val>Document.TextLength)
                    val = Document.TextLength;
                CaretOffset = val;
                TextArea.Caret.BringCaretToView();
                return false;
            }
            if (arg.Is("select_from"))
            {
                Select(arg.GetI("select_from"),arg.GetI("select_to"));
                return false;
            }

            return arg.Error("unknown args");
        }
    }




    /// <summary>A search result storing a match and text segment.</summary>
    public class SearchResult : TextSegment
    {
        /// <summary>The regex match for the search result.</summary>
        public Match Match { get; }

        /// <summary>Constructs the search result from the match.</summary>
        /// 
        public SearchResult(Match match)
        {
            this.StartOffset = match.Index;
            this.Length = match.Length;
            this.Match = match;
        }
    }

    /// <summary>Colorizes search results behind the selection.</summary>
    public class ColorizeSearchResultsBackgroundRenderer : IBackgroundRenderer
    {
        static void ok(object a) => MyFrame.ok(a);
        /// <summary>The search results to be modified.</summary>
        TextSegmentCollection<SearchResult> currentResults = new TextSegmentCollection<SearchResult>();

        /// <summary>Constructs the search result colorizer.</summary>
        public ColorizeSearchResultsBackgroundRenderer()
        {
            Background = new SolidColorBrush(Color.FromRgb(255, 181, 181));
            Background.Freeze();
        }

        /// <summary>Gets the layer on which this background renderer should draw.</summary>
        public KnownLayer Layer
        {
            get
            {
                // draw behind selection
                return KnownLayer.Selection;
            }
        }

        /// <summary>Causes the background renderer to draw.</summary>
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");
            if (drawingContext == null)
                throw new ArgumentNullException("drawingContext");

            if (currentResults == null || !textView.VisualLinesValid)
                return;

            var visualLines = textView.VisualLines;
            if (visualLines.Count == 0)
                return;

            int viewStart = visualLines.First().FirstDocumentLine.Offset;
            int viewEnd = visualLines.Last().LastDocumentLine.EndOffset;
            
            
            foreach (SearchResult result in currentResults.FindOverlappingSegments(viewStart, viewEnd - viewStart))
            {
                BackgroundGeometryBuilder geoBuilder = new BackgroundGeometryBuilder();
                geoBuilder.AlignToWholePixels = true;
                geoBuilder.BorderThickness = 0;
                geoBuilder.CornerRadius = 0;
                geoBuilder.AddSegment(textView, result);
                Geometry geometry = geoBuilder.CreateGeometry();
                if (geometry != null)
                {
                    drawingContext.DrawGeometry(Background, null, geometry);
                }
            }
        }

        /// <summary>Gets the search results for modification.</summary>
        public TextSegmentCollection<SearchResult> CurrentResults
        {
            get { return currentResults; }
        }

        /// <summary>Gets or sets the background brush for the search results.</summary>
        public Brush Background { get; set; }
    }

    public class MarkSameWord : DocumentColorizingTransformer
    {
        private readonly string _selectedText;

        public MarkSameWord(string selectedText)
        {
            _selectedText = selectedText;
        }

        protected override void ColorizeLine(DocumentLine line)
        {
            if (string.IsNullOrEmpty(_selectedText))
            {
                return;
            }

            int lineStartOffset = line.Offset;
            string text = CurrentContext.Document.GetText(line);
            int start = 0;
            int index;

            while ((index = text.IndexOf(_selectedText, start, StringComparison.Ordinal)) >= 0)
            {
                ChangeLinePart(
                  lineStartOffset + index, // startOffset
                  lineStartOffset + index + _selectedText.Length, // endOffset
                  element => element.TextRunProperties.SetBackgroundBrush(Brushes.Navy));
                start = index + 1; // search for next occurrence
            }
        }
    }

}