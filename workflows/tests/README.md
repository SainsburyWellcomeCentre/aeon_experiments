# Quality control / calibration / benchmarks

This folder is intended to contain auxiliary experiments related to Aeon experiments, e.g. standalone experiments used to calibrate or test hardware.

Folder structure should be:

- [ test-name ]
  - analysis
  - bonsai
  - qc-workflows

Where test-name is the name of the test, `bonsai` contains the Bonsai environment used for worklfows, `qc-workflows` contains the workflows themselves, `analysis` contains any python notebooks used for analysing the results.