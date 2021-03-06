# Score tabel

### Data

- 1 tile pair is worth 100 points

- board will have multiple layouts
    - 4 * 4 = 16 tiles
        - minimum score of 16 tiles * 100 points each 1600
    - 6 * 6 = 36 tiles
        - minimum score of 36 tiles * 100 points each 3600

### Score depletion

Per tick the score will be affected by -1 point

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

#### 6 * 6
|  Remaining Tiles  | Times    |     Points      |
|-------------------|----------|-----------------|
|  36               | 6        |  100 + 100 * 6  |
|  34               | 6        |  100 + 100 * 6  |
|  32               | 5        |  100 + 100 * 5  |
|  30               | 5        |  100 + 100 * 5  |
|  28               | 4        |  100 + 100 * 4  |
|  26               | 4        |  100 + 100 * 4  |
|  24               | 4        |  100 + 100 * 4  |
|  22               | 3        |  100 + 100 * 3  |
|  20               | 3        |  100 + 100 * 3  |
|  18               | 3        |  100 + 100 * 3  |
|  16               | 3        |  100 + 100 * 3  |
|  14               | 2        |  100 + 100 * 2  |
|  12               | 2        |  100 + 100 * 2  |
|  10               | 2        |  100 + 100 * 2  |
|  4                | 2        |  100 + 100 * 2  |
|  8                | 2        |  100 + 100 * 2  |
|  6                | 2        |  100 + 100 * 2  |
|  2                | 0        | no streak bonus |