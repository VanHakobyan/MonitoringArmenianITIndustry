import types from "store/types";
//by page
export const requestProfile = (id, social) => ({ type: types.REQUESTED_PROFILE, id, social });
export const succeededProfile = profile => ({ type: types.SUCCEEDED_PROFILE, profile });
export const failedProfile = error => ({ type: types.FAILED_PROFILE, error });

export const requestCrossProfile = () => ({ type: types.REQUEST_CROSS_PROFILE });
export const succeededCrossProfile = profiles => ({ type: types.SUCCEEDED_CROSS_PROFILE, profiles });
export const failedCrossProfile = error => ({ type: types.FAILED_CROSS_PROFILE, error });