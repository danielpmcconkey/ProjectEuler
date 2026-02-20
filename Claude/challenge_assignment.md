# Reverse-Engineered Problem Statements

## Euler0015 — "Lattice paths"

### Problem Statement

Given a rectangular grid of size n x m (where the grid has n columns and m rows of squares), you start at the top-left corner and must travel to the bottom-right corner. At each step, you may only move **right** or **down** (never left or up). Count the total number of distinct routes from the top-left to the bottom-right of the grid.

The specific challenge is to solve this for a **20 x 20** grid.

**Evidence:** The title is declared as `"Lattice paths"` at `Euler0015.cs:12`. The grid size is set to 20 at line 90 (`int gridWidth = 20;`) and the recursive call uses `CountRoutes(20, 20)` at line 217. The constraint that movement is only rightward or downward is described extensively in the comments at lines 27-30: *"every step must be either a right-ward movement or a down-ward movement"*.

### Specific Numerical Parameters

- **Grid size:** 20 x 20 (line 90: `int gridWidth = 20;` and line 217: `CountRoutes(20, 20)`)
- **Known validation values** (from the table at lines 54-71):
  - 2x2 grid: 6 routes
  - 3x3 grid: 20 routes
  - 4x4 grid: 70 routes
  - 10x10 grid: 184,756 routes
  - 15x15 grid: 155,117,520 routes

### Key Mathematical Insight

The number of lattice paths through an a x b grid satisfies the recurrence relation:

> `routes(a, b) = routes(a-1, b) + routes(a, b-1)`

with base case `routes(a, 0) = routes(0, b) = 1`.

This is stated explicitly in the comment at lines 211-213: *"The number of routes in an A x B square is equal to the number of routes in an (A - 1) x B rectangle plus the number of routes in an A x (B - 1) rectangle."*

The code implements this at line 233:
```csharp
cache.Add((a, b), CountRoutes(a - 1, b) + CountRoutes(a, b - 1));
```

The base case is at line 228:
```csharp
if (a == 0 || b == 0) return 1;
```

This works because: at any grid position, the remaining path to the corner is either through the cell to the right (an `(a-1) x b` subgrid) or the cell below (an `a x (b-1)` subgrid). When either dimension is 0, there's only one path — a straight line along the remaining edge.

This is mathematically equivalent to computing the central binomial coefficient C(2n, n) = C(40, 20) for a 20x20 grid, though the developer arrived at the combinatorial identity empirically rather than recognizing it directly.

### Evolution of the Developer's Thinking

**Attempt 1 — Brute force binary enumeration (`Run_bruteForce`, lines 109-134):**

The developer's first insight was that any path through an n x n grid consists of exactly `2n` steps — `n` rightward and `n` downward — and can therefore be encoded as a binary string of length `2n` where `1` represents "right" and `0` represents "down" (lines 146-148: *"6-digit binary number with 3 1s (representing right-ward movement) and 3 0s (representing down-ward movement)"*). A valid path is any binary string with exactly `n` ones.

The brute force approach iterates over all `2^(2n)` binary numbers and counts those with exactly `n` ones (lines 117-130). This worked up to n=15 but was infeasible for n=20 because `2^40 = 1,099,511,627,776` (line 49: *"2^40 is 1,099,511,627,776"*).

**Attempt 2 — Empirical pattern discovery (`Run_originalSolution`, lines 22-108):**

Using the brute force results for n=2 through n=15, the developer took the data into Google Sheets (line 73: *"I took it into Google sheets"*). They divided each answer by its predecessor and noticed the ratio approaches 4 (line 77: *"that result was approaching 4"*). After multiplying by n, they got integers increasing by 4, leading to the recurrence:

```
routes(n) = routes(n-1) * (4n - 2) / n
```

This is implemented at lines 97-99:
```csharp
double mysteryCoefficient = (n * 4) - 2;
double multiplier = mysteryCoefficient / n;
numberOfPossibleRoutes = (long)Math.Round(priorAnswer * multiplier, 0);
```

The developer candidly admits in the comment at line 84: *"Did I cheat? Yeah. Probably."*

**Attempt 3 — Recursive decomposition with memoization (`Run_recurrsion`, lines 135-235):**

This is the final and currently active solution (line 18: `Run_recurrsion()` is the uncommented call). The developer extended the analysis beyond square grids (lines 196-209) and discovered that `routes(a, b) = routes(a-1, b) + routes(a, b-1)`. This is the Pascal's triangle / binomial coefficient recurrence.

The developer added a dictionary-based memoization cache (line 8: `Dictionary<(int a, int b), long> cache;`, used at lines 229-234) to avoid recomputing overlapping subproblems.

---

## Euler0086 — "Cuboid route"

### Problem Statement

Consider a rectangular cuboid (box) with integer dimensions width, height, and depth. A spider sits at one corner of the cuboid (call it S) and a fly sits at the diagonally opposite corner (call it F). The spider can only walk along the surfaces of the cuboid. Find the shortest surface path from S to F.

A solution is called an "integer solution" if this shortest surface path has an integer length. Let M be the maximum allowed dimension. Consider all cuboids with dimensions `1 <= depth <= height <= width <= M`. Find the least value of M such that the number of cuboids with integer shortest-surface-paths first exceeds **one million** (1,000,000).

**Evidence:** The title is `"Cuboid route"` at `Euler0086.cs:8`. The target is defined at line 103: `var target = 1000000;`. The problem mentions a spider and fly at lines 16-18: *"If the spider needs to go from (S) to (F)"*. The termination condition is at lines 126-131: `if(numberOfIntegerSolutions > target)`, after which the answer printed is `m` — the maximum dimension M.

**Validation values** referenced in comments at lines 37-38: *"the problem statement said that M of 99 gives 1975 integer solutions and M of 100 gives 2060"*.

### Specific Numerical Parameters

- **Target:** 1,000,000 integer solutions (line 103: `var target = 1000000;`)
- **Validation checkpoints** (lines 37-38, 64-65):
  - M = 99 produces 1,975 integer solutions
  - M = 100 produces 2,060 integer solutions
- **Dimension constraint:** `1 <= depth <= height <= width <= M` (enforced in `Run_slow` at lines 170-173: `height` from 1 to m, `depth` from height to m, with `width = m`)
- **The answer is the value of M** (line 129: `PrintSolution(m.ToString())`)

### Key Mathematical Insight

The shortest surface path between opposite corners of a cuboid can be found by **unfolding** the box flat. When you unfold a cuboid with dimensions w, h, d, there are three possible unfoldings corresponding to three pairs of faces the spider could traverse. Each unfolding produces a simple 2D straight-line distance:

1. `sqrt(w^2 + (h+d)^2)` — across the width face, then a face combining height and depth
2. `sqrt((w+h)^2 + d^2)` — combining width and height, then across the depth face
3. `sqrt((w+d)^2 + h^2)` — combining width and depth, then across the height face

The shortest path is the minimum of these three.

**Evidence for the unfolding insight** is the ASCII diagram at lines 51-61, showing a cuboid net (cross-shaped unfolding), and the comment at lines 47-49: *"Someone was trying to find distances between cuboid vertices and an example someone gave was a cardboard box you could unfold."*

The `Run_slow` function computes all three unfoldings explicitly at lines 152-154:
```csharp
var min = getHypotenuse(width, height + depth);
min = Math.Min(min, getHypotenuse(width + height, depth));
min = Math.Min(min, getHypotenuse(width + depth, height));
```

The **critical optimization** in `Run_fast` is the recognition that when `w` is the largest dimension (i.e., `w >= h >= d`), the minimum unfolding is always `sqrt(w^2 + (h+d)^2)`. This is because `w` is the largest side, so keeping it unsplit always gives the smallest hypotenuse. This eliminates the need to check all three unfoldings.

Evidence: In `Run_fast` (lines 96-133), the function `isShortestRouteAnInt` at lines 98-102 only computes a single distance:
```csharp
var min = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(yPlusZ, 2));
```
where `x` is the largest dimension and `yPlusZ` is the sum of the other two.

The second optimization is collapsing the two inner loops (over height and depth separately) into a single loop over their **sum** `yPlusZ` (line 110: `for (int yPlusZ = 1; yPlusZ <= x + x; yPlusZ++)`). For each value of `yPlusZ`, the code counts how many valid `(height, depth)` pairs produce that sum, subject to `1 <= depth <= height <= width`. This count is computed at lines 114-122:

```csharp
if(yPlusZ > x + 1)
{
    yzCombinations = (x + x + 2 - yPlusZ) / 2;
}
else
{
    yzCombinations = yPlusZ / 2;
}
```

The logic here: when `yPlusZ` is small (at most `x + 1`), the number of pairs `(h, d)` with `1 <= d <= h` and `h + d = yPlusZ` is `floor(yPlusZ / 2)` — since d can range from 1 to `floor(yPlusZ/2)` with h = yPlusZ - d. When `yPlusZ > x + 1`, the constraint `h <= x` becomes binding, which reduces the count. For example, if yPlusZ = 2x, then h = d = x is the only option (1 combination). The formula `(2x + 2 - yPlusZ) / 2` handles this reduction.

The upper bound on `yPlusZ` is `2 * x` (line 110: `yPlusZ <= x + x`) because both `h` and `d` are at most `x` (since `x` is the maximum dimension M for this iteration).

### Evolution of the Developer's Thinking

**Attempt 1 — Two right triangles with linear regression (described at lines 15-35, code not preserved):**

The developer first imagined the spider walking diagonally up one wall and then across an adjacent wall, meeting at a point A on the edge between them. This creates two right triangles (SHA and AGF, per the 3D diagram at lines 21-28). The developer tried to find the optimal split point A using a *"clever linear regression technique"* (line 31) to minimize the total distance in about 5-10 steps. This was then repeated for all six possible wall-pair combinations.

This approach gave *"very promising results...But not entirely accurate results"* (lines 34-35). The problem manifested as floating-point precision issues: the developer could match either the M=99 or M=100 validation value by changing rounding, but never both simultaneously (lines 39-42: *"By playing with how I rounded my answers, I could get either of those, but never both"*). The developer spent *"two full weekend days"* (line 40) trying to fix this before giving up.

**Attempt 2 — Unfolding the box (`Run_slow`, lines 135-195):**

Inspired by a tip about unfolding a cardboard box (lines 46-49), the developer switched to computing the straight-line distance on the unfolded net. This immediately gave correct validation values (lines 64-65: *"At M = 99 I got 1975. At M = 100, I got 2060"*).

However, the triple-nested loop (M * M * M roughly) with the target of 1,000,000 was too slow. The developer first tried memoizing based on dimension ratios, but this consumed too much RAM around 800k solutions (lines 76-79: *"I eventually filled up my RAM with my dictionary"*). Switching to memoizing the hypotenuse function (line 136: `Dictionary<(int, int), double> hypotenuses`) brought runtime down to ~6 minutes (lines 83-84: *"brought execution time down to around 6 minutes"*).

**Attempt 3 — Collapsing dimensions (`Run_fast`, lines 96-133):**

The final optimization, currently active (line 94: `Run_fast()`), eliminates the inner two loops entirely by iterating over `yPlusZ` (the sum of the two smaller dimensions) instead of `height` and `depth` individually. For each value of `yPlusZ` that produces an integer hypotenuse, the code algebraically counts how many `(h, d)` decompositions exist (lines 114-122) rather than enumerating them. This reduces the problem from O(M^3) to O(M^2), and within that, only Pythagorean-triple checks are needed.
