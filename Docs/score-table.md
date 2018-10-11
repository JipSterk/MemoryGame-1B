# Score tabel

### Data

- 1 tile pair is worth 100 points

- board will have multiple layouts
    - 4 * 4 = 16 tiles
        - minimum score of 16 tiles * 100 points each 1600
    - 6 * 6 = 36 tiles
        - minimum score of 36 tiles * 100 points each 3600

### StreakBonus

#### 4 * 4
|  Remaining Tiles  | Times    |     Points      |
|-------------------|----------|-----------------|
|  16               | 5        |  100 + 100 * 5  |
|  14               | 4        |  100 + 100 * 4  |
|  12               | 3        |  100 + 100 * 3  |
|  10               | 3        |  100 + 100 * 3  |
|  4                | 2        |  100 + 100 * 2  |
|  8                | 2        |  100 + 100 * 2  |
|  6                | 2        |  100 + 100 * 2  |
|  2                | 0        | no streak bonus |