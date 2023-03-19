import os
import sys
import clr
ref = clr.AddReference("KalmanCommutator")

ASSEMBLY_PATH = ref.Location
PACKAGE_ROOT_PATH = os.path.abspath(os.path.join(ASSEMBLY_PATH ,"../../.."))
print(PACKAGE_ROOT_PATH)
DEFAULT_CONFIG_PATH = f"{PACKAGE_ROOT_PATH}/Library/metadata/00000009_smoothing.ini"

sys.path.append(os.path.abspath(f"{PACKAGE_ROOT_PATH}/Library"))
sys.path.append(os.path.abspath(f"{PACKAGE_ROOT_PATH}/Scripts"))

import configparser
import numpy as np
import utils, learning, simulation, inference


class KalmanFilter:

    def __init__(self,
                 fps: int = 20,
                 filtering_params_filename: str = DEFAULT_CONFIG_PATH,
                 ) -> None:

        # configuration variables
        filtering_params_section = "params"

        # get confi params
        filtering_params = configparser.ConfigParser()
        filtering_params.read(filtering_params_filename)
        pos_x0 = float(filtering_params[filtering_params_section]["pos_x0"])
        pos_y0 = float(filtering_params[filtering_params_section]["pos_y0"])
        vel_x0 = float(filtering_params[filtering_params_section]["vel_x0"])
        vel_y0 = float(filtering_params[filtering_params_section]["vel_x0"])
        acc_x0 = float(filtering_params[filtering_params_section]["acc_x0"])
        acc_y0 = float(filtering_params[filtering_params_section]["acc_x0"])
        sigma_ax = float(filtering_params[filtering_params_section]["sigma_ax"])
        sigma_ay = float(filtering_params[filtering_params_section]["sigma_ay"])
        sigma_x = float(filtering_params[filtering_params_section]["sigma_x"])
        sigma_y = float(filtering_params[filtering_params_section]["sigma_y"])
        sqrt_diag_V0_value = float(filtering_params[filtering_params_section]["sqrt_diag_V0_value"])

        if np.isnan(pos_x0):
            pos_x0 = 0
        if np.isnan(pos_y0):
            pos_y0 = 0

        # build KF matrices for tracking
        dt = 1.0 / fps
        # Taken from the book
        # barShalomEtAl01-estimationWithApplicationToTrackingAndNavigation.pdf
        # section 6.3.3
            # Eq. 6.3.3-2
        B = np.array([[1, dt, .5*dt**2, 0, 0, 0],
                       [0, 1, dt, 0, 0, 0],
                       [0, 0, 1, 0, 0, 0],
                       [0, 0, 0, 1, dt, .5*dt**2],
                       [0, 0, 0, 0, 1, dt],
                       [0, 0, 0, 0, 0, 1]],
                      dtype=np.double)
        Z = np.array([[1, 0, 0, 0, 0, 0],
                       [0, 0, 0, 1, 0, 0]],
                      dtype=np.double)
            # Eq. 6.3.3-4
        Qt = np.array([[dt**4/4, dt**3/2, dt**2/2, 0, 0, 0],
                       [dt**3/2, dt**2,   dt,      0, 0, 0],
                       [dt**2/2, dt,      1,       0, 0, 0],
                       [0, 0, 0, dt**4/4, dt**3/2, dt**2/2],
                       [0, 0, 0, dt**3/2, dt**2,   dt],
                       [0, 0, 0, dt**2/2, dt,      1]],
                      dtype=np.double)
        R = np.diag([sigma_x**2, sigma_y**2])
        m0 = np.array([pos_x0, 0, 0, pos_y0, 0, 0], dtype=np.double)
        V0 = np.diag(np.ones(len(m0))*sqrt_diag_V0_value**2)
        Q = utils.buildQfromQt_np(Qt=Qt, sigma_ax=sigma_ax, sigma_ay=sigma_ay)

        # filter
        self.onlineKF = inference.OnlineKalmanFilter(B=B, Q=Q, m0=m0, V0=V0, Z=Z, R=R)

    def predict(self, x, y):
        _, _ = self.onlineKF.predict()
        ret = self.onlineKF.update(y=[x, y])
        return ret
