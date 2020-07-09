using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyPatternDynamicNamespace
{

    public enum OutputFormat
    {
        Html,
        Markdown
    }

    public interface IListStrategy
    {
        void Start(StringBuilder stringBuilder);
        void End(StringBuilder stringBuilder);
        void AddListItem(StringBuilder stringBuilder, string item);
    }

    public class HtmlStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder stringBuilder, string item)
        {
            stringBuilder.AppendLine($" <li>{item}</li>");
        }

        public void End(StringBuilder stringBuilder)
        {
            stringBuilder.Append("</ul>");
        }

        public void Start(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("<ul>");
        }
    }

    public class MarkdownStrategy : IListStrategy
    {
        public void AddListItem(StringBuilder stringBuilder, string item)
        {
            stringBuilder.AppendLine($" * {item}");
        }

        public void End(StringBuilder stringBuilder)
        {

        }

        public void Start(StringBuilder stringBuilder)
        {

        }
    }

    public class TextProcessor
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();
        IListStrategy listStrategy;

        public void SetOutputFormat(OutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case OutputFormat.Html:
                    listStrategy = new HtmlStrategy();
                    break;
                case OutputFormat.Markdown:
                    listStrategy = new MarkdownStrategy();
                    break;
                default:
                    break;
            }
        }

        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(stringBuilder);
            foreach (var item in items)
            {
                listStrategy.AddListItem(stringBuilder, item);
            }
            listStrategy.End(stringBuilder);
        }

        public StringBuilder Clear()
        {
            return stringBuilder.Clear();
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }

   

    class StrategyPatternDynamic
    {
        static void Main(string[] args)
        {
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(new []{ "foo", "bar", "markdown" });
            Console.WriteLine(tp);

            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(new[] { "foo", "bar", "html" });
            Console.WriteLine(tp);
        }
    }
}
