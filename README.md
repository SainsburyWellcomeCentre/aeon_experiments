# Project Aeon - Experiments

The Project Aeon experiments repository contains the set of standardized protocols, operation instructions and metadata necessary for reproducible task control and acquisition of the foraging arena assay. The scripts contained in this repository should always represent as accurately as possible the automation routines and operational instructions used to log the experimental raw data for Project Aeon. Each acquired dataset should have a reference to the specific hash or release from this repository which was used in the experiment.

## Deployment Instructions

The Project Aeon acquisition framework runs on the [Bonsai](https://bonsai-rx.org/) visual programming language. This repository includes installation scripts which will automatically download and configure a reproducible, self-contained, Bonsai environment to run all acquisition systems on the foraging arena. It is necessary, however, to install a few system dependencies and device drivers which need to be installed separately, before runnning the environment configuration script.

### Prerequisites

These should only need to be installed once on a fresh new system, and are not required if simply refreshing the install or deploying to a new folder.

 * Windows 10
 * [Visual Studio Code](https://code.visualstudio.com/) (recommended for editing code scripts and git commits)
 * [.NET Framework 4.7.2 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer) (required for intellisense when editing code scripts)
 * [Python 3.10](https://www.python.org/downloads/release/python-31011/) (required for bootstrapping Python environments)
 * [Git for Windows](https://gitforwindows.org/) (recommended for cloning and manipulating this repository)
 * [Visual C++ Redistributable for Visual Studio 2012](https://www.microsoft.com/en-us/download/details.aspx?id=30679) (native dependency for OpenCV)
 * [FTDI CDM Driver 2.12.28](https://www.ftdichip.com/Drivers/CDM/CDM21228_Setup.zip) (serial port drivers for HARP devices)
 * [Spinnaker SDK 1.29.0.5](https://www.flir.co.uk/support/products/spinnaker-sdk/#Downloads) (device drivers for FLIR cameras)
   * On FLIR website: `Download > archive > 1.29.0.5 > SpinnakerSDK_FULL_1.29.0.5_x64.exe`
 * [CUDA 11.3](https://developer.nvidia.com/cuda-11.3.0-download-archive) (for SLEAP multi-animal tracking)
   * Select Custom install and check `CUDA > Development` and `CUDA > Runtime` ONLY (uncheck everything else)

### Hardware Setup

The current workflows are designed to work with [FLIR Spinnaker Blackfly S cameras, BFS-U3-16S2M](https://www.flir.com/products/blackfly-s-usb3/?model=BFS-U3-16S2M-CS), or alternatively the [Basler ace U acA1440-220um](https://www.baslerweb.com/en/products/cameras/area-scan-cameras/ace/aca1440-220um/) running on the Pylon drivers. Blackfly S cameras are configured by the experiment workflow itself. For Basler ace cameras it is necessary to export a config file from the pylon Viewer from the `Camera > Save Features...` menu. The config `*.pfs` can be used by the `PylonVideoSource` to ensure reproducible configurations for each camera. Each camera should be connected to the main acquisition computer using USB 3.1, ideally making sure there are no more than 2 or 3 cameras per USB hub in the computer. Cameras are assigned through their unique serial numbers, so the order of connection is not important.

Harp devices are synchronized in a hub and spoke topology using a [ClockSynchronizer](https://www.cf-hw.org/harp/clock-sync) board. Cameras are triggered simultaneously using one of two independent PWM pulses generated by an [OutputExpander](https://github.com/harp-tech/harp_expander) board (the `VideoController`). Hirose 6-pin GPIO cables can be used to connect the trigger line to the cameras.

Each patch is controlled independently by one [OutputExpander](https://github.com/harp-tech/harp_expander) board (`Patch1` and `Patch2`). These boards monitor the wheel encoder and control the [FED3](https://open-ephys.org/fed3/fed3) pellet dispenser. The FED3 is powered by USB to continously charge its battery, with a digital line coming from the Expander boards to trigger pellet delivery. Detection of whether a pellet has been delivered on the chute is done using a digital line from the collector of the photo-transistor built into the FED3 through a 1 kOhm pull-up resistor.

All these boards are connected to the computer and configured at the following COM ports:

| COM Port | Device Name       |
|----------|-------------------|
| COM6     | ClockSynchronizer |
| COM3     | VideoController   |
| COM4     | Patch1            |
| COM7     | Patch2            |

Automatic weighing of animals is performed using the [Ohaus Navigator NVT2201 Electronic Balance](https://us.ohaus.com/en-US/Products/Balances-Scales/Portable-Balances/Navigator/Electronic-Balance-NVT2201-AM) via their USB interface. This requires the following setup procedure to be done on the balance itself before it is connected to the system for the first time:

 1. Rotate the transportation lock located under the balance to the unlocked position.
 2. Level the balance to ensure the level indicator bubble on the top-right corner of the front panel is centered.
 3. Connect the USB Interface to the communication port located under the balance, and to the computer.
 4. Hold the Tare button until the display changes to `Menu` and then release the button. The display should now show `.C.A.L.`.
 5. Press the `Print / No`button until the menu shows `U.S.b.`.
 6. Press the `Zero / Yes` button once to start USB interface configuration. For each configuration parameter, pressing `Zero / Yes` confirms and advances to the next parameter, and `Print / No` cycles through the different options. Set the following parameters:
    * On-Off: On
    * Baud: 9600
    * Parity: 8-none
    * Handsh: none
    * End: End
7. Back to the main menu, press the `Print / No` button until the menu shows `Mode`.
8. Press the `Zero / Yes` button once to start mode configuration and set the following configuration:
    * Stable: Off
9. Back to the main menu, press the `Print / No` button until the menu shows `P.r.i.n.t`.
10. Press the `Zero / Yes` button once to start automatic print configuration and set the following parameters:
    * Stable: Off
    * A.Print: Cont
    * End: End

### Network Setup 

All AEON computers are currently running on a [VLAN](https://en.wikipedia.org/wiki/VLAN). The current network infrastructure requires manual configuration of IPV4 settings.

| IPV4 Address Range   | 172.24.158.1-199 |
| :------------------- | :--------------- |
| IPV4 Subnet Mask     | 255.255.254.0    |
| IPV4 Default Gateway | 172.24.158.245   | 
| IPV4 DNS Servers     | 192.168.238.208 <br/> 192.168.239.201| 

Below is the current IP address table:

| Host Name | IP             |
| :-------- | :------------- |
| AEON1     | 172.24.158.101 |
| AEON2     | 172.24.158.102 |
| AEON3     | 172.24.158.103 |

### Environment Setup

The `bonsai` folder contains a snapshot of the runtime environment required to run experiments on the foraging arena. The `setup.cmd` batch script is included in this repository to automate the download and configuration of this environment. Simply double-clicking on this script should launch the necessary powershell commands as long as an active connection to the internet is available.

In case the configuration of the environment ever gets corrupted, you can revert the `bonsai` folder to its original state by deleting all the executable and package files and folders and re-running the `setup.cmd` script. This process may be automated in the future.

### Post-checkout Hook

The `hooks` folder contains git hook scripts which can be installed in each local experiment repository to ensure that the environment is reset to the correct configuration whenever the repository switches to a different experimental branch. To install the hook, copy the `post-checkout` and `post-merge` files into the `.git\hooks` folder.

After this, the scripts should run automatically whenever you switch branches in the repository or pull changes with modifications to the `Bonsai.config` file. Note that if you do this inside a UI such as VS Code you might get limited feedback as to the progress of the environment reset, and it might be necessary to wait for a little bit until all packages are reinstalled.

It is recommended to install the post-checkout hook on all acquisition machines running experiments to maximize reproducibility when switching experiments or running updates.

### Incoming Webhooks

The system supports sending alerts to [Incoming Webhooks](https://learn.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/what-are-webhooks-and-connectors#incoming-webhooks) configured on a Slack or Teams channel. This is used during acquisition for live notifications of critical failures, warnings or other conditions of interest which might require manual intervention. Incoming Webhooks on Teams will accept any message formatted as a JSON payload complying with the [connector card schema for O365 Groups](https://learn.microsoft.com/en-us/microsoftteams/platform/task-modules-and-cards/cards/cards-reference#connector-card-for-microsoft-365-groups). Currently the `SendMessageCard` operator will generate the compliant JSON payload using Markdown formatted text.

Incoming Webhooks can be created directly using the [Teams user interface](https://learn.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/add-incoming-webhook?tabs=dotnet#create-an-incoming-webhook-1). Each acquisition computer currently has two configured webhooks, one for Alert messages and another for Status messages. Note that only channel admins in Teams are allowed to create and manage Incoming Webhooks.

HTTPS endpoints for each specific machine name are stored in the `config` folder in CEPH, in the `alerts.config` and `status.config` files respectively. CEPH permissions are required to update these files.

### Data Transfer

Data is continuously transferred to a CEPH partition by calling Robocopy from a scheduled task which runs periodically every hour. This task should be started as soon as the computer boots. For each experiment, a script to launch the periodic task is provided with the following naming convention `SystemStartup-<machine_name>.cmd`. This script should be installed in a new computer using the following steps:

  1. Press `Windows logo key + R` and type `shell:startup` in the run dialog box to open the system startup folder.
  2. Drag the script to the startup folder while holding the `Alt` key to create a new startup link.
  3. Press `Alt + Enter` in the new shortcut to access the link properties.
  4. Add `-p Synchronize=true` to the end of the `Target` string to ensure the Harp clock is reset to UTC time on system boot.
  5. Click the `OK` button to save the modified link properties.

The raw data folder name in CEPH should be the Host Name corresponding to the acquisition machine as specified in the table above. Each acquisition computer should be assigned a unique user name with exclusive write permissions over the CEPH folder to which it is writing. Care should be taken to ensure that all other users only have read permissions over the folder. The current recommended naming convention for users is `aeon_<machine_name>` all lowercase.

The remote CEPH folder can be subsequently mounted on the local machine as a network drive and accessed as part of the file system. For example, the data transfer script for Experiment 0.1 running on AEON2 currently assumes data is collected in `D:\ProjectAeon\experiment0.1` and backed up to a network mount at `Z:\experiment0.1` which corresponds to CEPH partition `/aeon/data/raw/AEON2/experiment0.1/`.

### Calibration Targets

To allow spatial registration of video data, all cameras used in Project Aeon need to be calibrated against a series of targets to extract both intrinsic and extrinsic parameters.

Calibration of camera intrinsics requires a 13x9 checkerboard of an appropriate scale for the camera sensor and lens focal distance.

Checkerboard patterns were generated at [calib.io](https://calib.io/pages/camera-calibration-pattern-generator) with the following parameters:

| Camera | Board Size (mm) | Checker Size (mm) |
|--------| --------------- |------------------ |
| `Top`  | 420 x 594       | 40                |
| `Side` | 85.6 x 53.98    | 5                 |

The larger `Top` pattern was printed into an Aluminium (Dibond) sheet with straight edges to ensure a rigid flat surface.

The smaller `Side` pattern was printed onto the back of a blank ID card using an ID card printer.

### Camera Intrinsics

Extraction of camera intrinsics was performed using OpenCV calibration routines in Python. The `python` folder contains scripts which can be used to bootstrap an environment compatible with the acquisition setup.

In addition, if using Windows 10 N editions or LTSR, the [media feature pack](https://support.microsoft.com/en-us/topic/media-feature-pack-list-for-windows-n-editions-c1c6fffa-d052-8338-7a79-a4bb980a700a) may need to be installed in advance.

Below are details for the specific environments we have tested:
  * Windows LTSR : Version 1807 (AEON2 - Download from microsoft support)
  * Windows 10 N: Version 20H2 (latest AEON1 - Apps & Features > Add a feature)
  * Windows 10 Enterprise LTSC: Version 21H2 (latest AEON3 - Apps & Features > Add a feature)
  
### Room Light Controller

Room lights can be dynamically controlled using a serial over ethernet protocol and the [ES-257 Ethernet to Serial](https://www.brainboxes.com/product/ethernet-to-serial/es-poe/es-257) from [Brainboxes](https://www.brainboxes.com/). These provide addressable control of built-in light panels. Each one of the panels houses three different light temperatures (warm white, cold white, red). Each combination of color channel and panel has a unique address in the system. There are two available ports to connect on the ES-257, and each port allows access to full light control on all four rooms.

The drivers can be obtained from the [Brainboxes website support page](https://www.brainboxes.com/faq/where-can-i-find-the-windows-drivers-for-my-ethernet-to-serial). At the time of writing we are using Boost.LAN Driver Version 4.3.284.0.

After installation, the network should be scanned for devices using Boost.LAN Manager File > "Find Devices". The ES-257 should be automatically detected and displayed in the list. The virtual COM ports then need to be installed by selecting the device and the option "Install Virtual COM Port". After successful configuration, the ports should be listed in Windows device manager.

![Virtual COM Ports](https://user-images.githubusercontent.com/5315880/191293434-4723812d-f16f-41b1-a40c-f982686277a4.png)

### Experiment Workflows

All experiment-specific workflows required to run the experiment are stored in the `workflows` folder. We strongly recommend that each experiment-specific branch includes a `README.md` file describing what the protocol is about and how to configure or run it. The `.gitignore` file may be modified to exclude files according to the needs of each experiment.

It is recommended to keep every project within its own subfolder, and name the subfolder with the same name as the branch name. Multiple sub-folders are allowed if strict project separation is required, e.g. workflows running on different machines with different environments or incompatible extensions.

## Citation Policy

If you use this software, please cite it as below:

Sainsbury Wellcome Centre Foraging Behaviour Working Group. (2023). Aeon: An open-source platform to study the neural basis of ethological behaviours over naturalistic timescales,  https://doi.org/10.5281/zenodo.8413142

[![DOI](https://zenodo.org/badge/485512362.svg)](https://zenodo.org/badge/latestdoi/485512362)
