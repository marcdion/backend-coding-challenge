# Scoring algorithm

The scoring algorithm is based on three base factors and one optional factor:

- Population
- Popularity
- Edit distance
- Coordinate distance (Optional)

When inserting data in the prefix tree, locations are entered base on their population. we figure that a more populated city will have more interest. 

The second parameter is the popularity factor (i.e. the number of times a location has been searched). This scoring method is pretty rudimentary and is one of the areas I would like to improve in a subsequent version of the API. 

The third is the edit distance of a word. So the less characters are missing to complete the location name starting from the query, the higher the score will be. This is base on a logarithmic function.

The last parameter is the coordinates distance. If some coordinates are provided in the query, the Haversine equation is used to calculate the distance between the two sets of coordinates.The closer the location is, the higher the score will be.

The weight of the scores are atributed at the start of the API. The can be tuned in the **/api/src/Domain/Models/ScoringWeights/SharedScoringWeight.cs** class.