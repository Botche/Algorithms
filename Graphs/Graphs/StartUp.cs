namespace Graphs
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            // The example is taken from exercises
            Graph graph = new Graph(9);

            // When initializing it takes information from specific file based on type matrix passed as parameter
            // graph.InitializeMatrix(GlobalConstants.ADJACENCY_MATRIX);
            // graph.InitializeMatrix(GlobalConstants.LIST_OF_EDGES);
            graph.InitializeMatrix(GlobalConstants.INCIDENCE_MATRIX);

            graph.SaveInFileMatrixAsAdjacency();
            graph.SaveInFileListOfEdges();
            graph.SaveInFileMatrixAsIncidence();
        }
    }
}
