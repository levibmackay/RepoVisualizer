# RepoAnalyzer

A recursive C# repository analysis tool that scans directory structures, generates Markdown reports, tracks file type distributions, identifies large files, and visualizes folder hierarchies.

## Overview

I built this project while learning recursion in C#. Rather than creating a simple recursion exercise, I wanted to apply recursive tree traversal to a real-world problem that developers encounter every day: understanding the structure of a codebase.

RepoAnalyzer recursively explores folders, gathers information about files, and generates a summary report that makes it easier to understand the contents of a repository.

## Features

* Recursive directory traversal
* Folder hierarchy visualization
* Markdown report generation
* File type breakdown by extension
* Largest file detection
* Folder size calculations
* Automatic filtering of common build and system folders

  * `bin`
  * `obj`
  * `.git`
  * `.vs`
  * `node_modules`
* Graceful handling of inaccessible directories

## Example Output

### Console Output

```text
📂 Projects
  📂 RepoAnalyzer
    📂 Models
    📂 Services
  📂 GymBroGuide
```

### Markdown Report

```markdown
# Repository Analyzer Report

- 📂 RepoAnalyzer (2.15 MB)
  - 📄 Largest File: Program.cs (14.52 KB)
  - 📊 File Breakdown:
    - .cs: 18
    - .json: 3
    - .md: 1
```

## Technologies Used

* C#
* .NET
* Recursive algorithms
* File I/O
* Dictionaries
* Exception handling
* StringBuilder
* Markdown generation

## What I Learned

This project helped me gain practical experience with:

* Recursion and tree traversal
* Working with the file system in C#
* Aggregating and summarizing data
* Using dictionaries to categorize information
* Building developer-focused tools
* Generating reports programmatically

One of the biggest takeaways from this project was seeing recursion applied to a real-world problem instead of a textbook example. Building a tool that traverses and analyzes an entire directory structure made the concept much easier to understand.

## Future Improvements

* Repository-wide summary statistics
* CSV export
* Extension percentage analysis
* Largest folders report
* Search and filtering capabilities
* Interactive console interface
* Visualization graphs and charts

## Running the Project

1. Clone the repository.
2. Open the project in your preferred C# IDE.
3. Update the target directory path in `Program.cs`.
4. Run the application.
5. Review the generated `report.md` file.

## Author

Levi Mackay

Computer Science student at BYU–Idaho interested in software engineering, developer tools, and building practical projects that improve programming workflows.

_Last updated: July 22, 2026_

_Last reviewed: 2026-07-20 19:33 MDT_

---
**Last updated:** 2026-07-21
