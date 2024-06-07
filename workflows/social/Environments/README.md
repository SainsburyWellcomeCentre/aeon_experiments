## Randomly Sampled Environments

This folder contains dynamic YAML configuration files to be sampled. The workflow will randomly sample from the list of files when manually reloading the environment and at midnight each day. If the list is empty, or if the sampled file is invalid, the current environment configuration will be maintained.

The default environment configuration will be used if no configuration file is loaded. In this case, the environment configuration name will be empty in the experiment metadata text box.