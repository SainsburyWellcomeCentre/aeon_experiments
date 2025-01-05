import pandas as pd

white1 =    pd.timedelta_range("00:00:00", periods=7*60, freq="1min")
twilight1 = pd.timedelta_range("07:00:00", periods=1*60, freq="1min")
dark =      pd.timedelta_range("08:00:00", periods=11*60, freq="1min")
twilight2 = pd.timedelta_range("19:00:00", periods=1*60, freq="1min")
white2 =    pd.timedelta_range("20:00:00", periods=4*60, freq="1min")

lightcycle = pd.DataFrame(
    {"Red": 0, "ColdWhite": 0, "WarmWhite": 0},
    index=pd.timedelta_range(0, periods=24*60, freq="1min", name="Minute"))

lightcycle.loc[white1] = 75
lightcycle.loc[twilight1] = 40
lightcycle.loc[dark] = 1
lightcycle.loc[twilight2] = 40
lightcycle.loc[white2] = 75
lightcycle.index = (lightcycle.index.total_seconds() / 60).astype('int32')

lightcycle.to_csv('lightcycle.config')