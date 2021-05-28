# Introduction
Thank y'all for taking the time to contibute!

This an open source project and we love to receive contributions from our community — you! There are many ways to contribute, from writing tutorials or blog posts, improving the documentation, submitting bug reports and feature requests or writing code which can be incorporated into the project itself.

Following these guidelines helps to communicate that you respect the time of the developers managing and developing this open source project. In return, they should reciprocate that respect in addressing your issue, assessing changes, and helping you finalize your pull requests.

### Important resources:
 - Overall information about TestIT: <https://testit.software/> 
 - TestIT URL for tests: <https://demo.testit.software/>

If you have any issues or you find someone's behavior unaccaptable, don't heistate to contact us at <support@testit.software>

# Set up your environment
In order to work with source code and run it you need:
 - Visual Studio 2019
 - VSIX components development kit (Visual Studio SDK) installed [How-to install](<https://docs.microsoft.com/en-us/visualstudio/extensibility/installing-the-visual-studio-sdk?view=vs-2019>)

Refer [README](README.md) file for details

# Reporting issues and feature requests
## Security issues
If you find a security vulnerability, do NOT open an issue. Email <support@testit.software> instead.

For example you got access to projects or data you're not supposed to have access to. That's definitely a security issue and we appreciate if you contact us directly. Even if you are not sure if that is a security issue, just email us at <support@testit.software>.

## Regular issues/suggestions
We kindly recommend you to follow issue template when filing a new one. Please, ensure that your statements are easy to understand and follow.

# Pull requests advices
Pull requests is the only acceptable contibution method.

## Make a pull request
 - You need a local fork of the repository in GitHub.
 - Use a separate 'feature' branch for your changes. The branch should be followed by the feature/ prefix and have an undestantandable name describing the mail goal of the changes<br>
<i>Ex: features/Added_support_for_new_API.</i><br>
    `git checkout -b features/Added_support_for_new_API`
 - Fetch the actual version from origin/master and rebase your feature branch onto it before creating a pull request.<br>
    `git fetch -p`<br>
    `git rebase origin/master`
 - Push the branch to Github<br>
    `git push -u origin features/Added_support_for_new_API`
 - Visit Github, you should see a proposal to create a pull request
 <br><br>
 - If you need to add new commits to the pull request, you can simply commit the changed to the local branch and then push them to update the pull request<br>
    `git commit -m "Addressed pull request comments"`<br>
    `git push -f`

## Keeping your pull request up-to-date
We use rebase flow to keep your code up-to-date. If a maintainer asks you to "rebase" your PR, they're saying that a lot of code has changed, and that you need to update your branch so it's easier to merge.

`git fetch -p`<br>
`git rebase origin/master`<br>
`git push`

## Code review
The core team looks at pull requests on a regular basis in a weekly triage meeting. After feedback has been given we expect responses within two weeks. After two weeks we may close the pull request if it isn't showing any activity.

## Merging a PR (maintainers only)
A PR can only be merged into master by a maintainer if:

 - It is passing CI.
 - It has been approved by at least two maintainers. If it was a maintainer who opened the PR, only one extra approval is needed.
 - It has no requested changes.
 - It is up to date with current master.
 - Any maintainer is allowed to merge a PR if all of these conditions are met.

# Code style
Consider the people who will read your code, and make it look nice for them.
## Basic rules
 - Declare namespaces <b>before</b> class declarations
 - Namespaces shoud follow folders hierarchy: `{ProjectName}.{Folder/Project name}.{Subfolder}`
 - Do not use "." in components' names
 - Avoid using partial classes
 - Methods should have meaningful names based on verbs: GetRecentItems, FormatDocument etc.
 - Consider declaring a set of smaller interfaces instead of a one huge
 - It is better to use a wider type. For example, use Collection instead of List.
 - Give clear argument names for methods: <br>
 Use `GetStepInfo(IStep step)` instead of `GetStepInfo(IStep entity)`
 - Add Async postfix to a method only in case there is a synchronous version of the method
 - Use no more than 120 characters per line
 - Remove unused namespaces, order namespaces alphabetically, but keep System namespaces at the first part
 - Separate methods with a single blank line
 - End files with an emply line
 - Use `== false` instead of "!"<br>
    `if (module.IsSuccessful == false)` instead of `if (!module.IsSuccessful)`
 - Prefer ICollection instead of IEnumerable to prevent possible multiple enumerations

## Line endings
We recommend to use LF line endings. This is how you can set them up for your git repo:<br>
`git config --global core.eol lf`<br>
`git config --global core.autocrlf input`<br>
`git rm -rf --cached .`<br>
`git reset --hard HEAD`


    