# agile-metrics

This is my personal training repo for practicing agile and clean coding.

## Iterations

Each iteration gets its own release. The master branch contains the road map and the initial repo.

* [ ] create an application `Domo` for displaying files from a file system.
  * show the directory-file hierarchy in an `Explorer` tree
* [ ] each node in the tree can have metrics
  * implement LOC for files (lines of code)
  * implement number of files for directories
  * implement display list of metric values for one node (e.g. the selected one)
* [ ] nodes can be tagged with categories
  * be aware of following rules
	* if all my children are X, I also become X
	* if I am X, all my children become also X
  * implement indicators on nodes
  * think about efficient data structures
* [ ] add computed metrics
  * implement metric `count of semicolon` / `LOC`
* [ ] add aggregated metrics on tag-filtered tree
  * implement `SUM of LOC` on directories
  * implement `median of LOC` on directories