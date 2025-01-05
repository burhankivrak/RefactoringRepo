using MongoDB.Bson;


namespace WorldConquerApp.Datalaag
{
    public class WorldModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int AlgorithmType { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public double Coverage { get; set; }
        public int[,] GeneratedWorld { get; set; }

        public List<ObjectId> EmpireIds { get; set; }
    }
}
