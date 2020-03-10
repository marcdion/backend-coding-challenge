# Improvement

The first improvement I would of made would be representing a more realistic popularity score for a location. Instead of simply incrementing the number of times a location has been queried, I would of like to store the score and the order it was in a suggestion list. This would give a better and more realisitc score. To make this better, I would of had stored theses values in a simple database.

The second improvement I would have done is tweak the edit distance score. Since it is based on a logarithmic function, a suggestions missing 1 character out of 16 has a higher score that a suggestion missing 1 character out of 3. Even though both words are almost complete, their scores are quite different.

The last improvement I would like to implement is a more robust unit test solution. Although there are some downsides to this, it makes the project more easily maintainable and scalable.

As mentionned in before, if this would of been a "real world" application, the front end and the API would of live in separated repositories to make the solution a true microservice architecture.