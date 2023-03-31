# Project Aeon - Experiments

The Project Aeon experiments repository contains the set of standardized protocols, operation instructions and metadata necessary for reproducible task control and acquisition of the foraging arena assay. The scripts contained in this repository should always represent as accurately as possible the automation routines and operational instructions used to log the experimental raw data for Project Aeon. Each acquired dataset should have a reference to the specific hash or release from this repository which was used in the experiment.

## Deployment Instructions

The Project Aeon acquisition framework runs on the [Bonsai](https://bonsai-rx.org/) visual programming language. This repository includes installation scripts which will automatically download and configure a reproducible, self-contained, Bonsai environment to run all acquisition systems on the foraging arena. It is necessary, however, to install a few system dependencies and device drivers which need to be installed separately, before runnning the environment configuration script.

### Prerequisites

These should only need to be installed once on a fresh new system, and are not required if simply refreshing the install or deploying to a new folder.

 * Windows 10
 * [Visual Studio Code](https://code.visualstudio.com/) (recommended for editing code scripts and git commits)
 * [.NET Framework 4.7.2 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer) (required for intellisense when editing code scripts)
 * [Git for Windows](https://gitforwindows.org/) (recommended for cloning and manipulating this repository)
 * [Visual C++ Redistributable for Visual Studio 2012](https://www.microsoft.com/en-us/download/details.aspx?id=30679) (native dependency for OpenCV)
 * [FTDI CDM Driver 2.12.28](https://www.ftdichip.com/Drivers/CDM/CDM21228_Setup.zip) (serial port drivers for HARP devices)
 * [Spinnaker SDK 1.29.0.5](https://www.flir.co.uk/support/products/spinnaker-sdk/#Downloads) (device drivers for FLIR cameras)
   * On FLIR website: `Download > archive > 1.29.0.5 > SpinnakerSDK_FULL_1.29.0.5_x64.exe`

### Hardware Setup

The current workflows are designed to work with Spinnaker Blackfly S cameras, model BFS-U3-16S2M. Each camera should be connected to the main acquisition computer using USB 3.1, ideally making sure there are no more than 2 or 3 cameras per USB hub in the computer. Cameras are assigned through their unique serial numbers, so the order of connection is not important.

Harp devices are synchronized in a hub and spoke topology using a [ClockSynchronizer](https://www.cf-hw.org/harp/clock-sync) board. Cameras are triggered simultaneously using one of two independent PWM pulses generated by an [OutputExpander](https://github.com/harp-tech/harp_expander) board (the `VideoController`).

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
The AEON computers are running on a VLAN.
The current setup requires a manual configuration of the IPV4 settings.
| IPV4 Adress | 172.24.158.1-199 |
|:-------|:------|
| IPV4 Subnet Mask | 255.255.254.0 |
| IPV4 Default Gateway | 172.24.158.245 | 
| IPV4 DNS Servers | 192.168.238.208 <br/> 192.168.239.201|  
 * AEON1 : 172.24.158.101
 * AEON2 : 172.24.158.102
 * AEON3 : 172.24.158.103

### Environment Setup

The `bonsai` folder contains a snapshot of the runtime environment required to run experiments on the foraging arena. The `setup.cmd` batch script is included in this repository to automate the download and configuration of this environment. Simply double-clicking on this script should launch the necessary powershell commands as long as an active connection to the internet is available.

In case the configuration of the environment ever gets corrupted, you can revert the `bonsai` folder to its original state by deleting all the executable and package files and folders and re-running the `setup.cmd` script. This process may be automated in the future.

### Data Transfer

Data is continuously transferred to a CEPH partition by calling Robocopy from a scheduled task which runs periodically every hour. This task is started as soon as the computer boots, using the OS task scheduler. Script with the task definitions for each experiment are versioned in this repository at `workflows\**\RobocopyAeon.xml`. These scripts can be installed in a new computer by opening the Task Scheduler app and selecting `Action > Import Task`.

For example, the data transfer script for Experiment 0.1 currently assumes data is collected in `D:\ProjectAeon\experiment0.1` and backed up to a network mount at `Z:\experiment0.1`.

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
  * Windows 10 Enterprise LTSC : Version 21H2 (AEON3 - Installed by It on 09/02/2023)
  * Windows LTSR : Version 1807 (AEON2 - Download from microsoft support)
  * Windows 10 N: Version 20H2 (latest AEON1 - Apps & Features > Add a feature)
