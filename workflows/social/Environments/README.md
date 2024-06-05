## Randomly Sampled Environments

This folder may contain dynamic YAML configuration files to be sampled. The workflow will randomly sample from the list of files at the start of the epoch and at midnight each day. If the list is empty, or if the sampled file is invalid, the current environment configuration will be maintained.

If at the start of the epoch there are no files in this folder, the default configuration file specified in the workflow properties will be used.

Environment configuration may also be changed manually, but it will be overridden by this random sampling process at midnight.