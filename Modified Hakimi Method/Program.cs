using Hakimi;

/**
 * Run methods on test data sets
 *
 * @param dataSetNumber - number of a data set (edged of a graph). Available values - from 0 to 3
 * @param algorithm - calculation algorithm. Available values:
 *      - BothMethods
 *      - HakimiMethod
 *      - ModifiedHakimiMethod
 *
 * Uncomment to run calculation with a test data
 */
//App.Test(1, App.TestAlgorithm.BothMethods);

/**
 * Run modified Hakimi method on a real data set
 *
 * @param inputPath - path to a file that contains a data set (edges of a graph)
 *      The table should look like this:
 *
 *        A  |  B  |  C  |  D  |  E  |  F  | ...
 *       ... | num | ... | num | ... | num | ...
 *       ... | num | ... | num | ... | num | ...
 *
 *       Where column "B" - vertex from, column "D" - vertex to, and "F" - weight of the edge
 *
 * @param outputPath - path to a file in which the distance matrix will be written in the form of CSV
 *
 * Uncomment to run calculation with a real data
 */
//App.Run(@"C:\SomePath\SomeFile.xls", @"C:\SomePath\SomeFile.csv");
