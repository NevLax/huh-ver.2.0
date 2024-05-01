namespace huh
{
    public class ViewGraph
    {
        public List<GraphField> graphs { get; set; }
        public ViewGraph(List<GraphField> fields)
        {
            graphs = new List<GraphField>();
            graphs.AddRange(fields);
        }
    }
}