import types from "store/types";
//by page
export const requestJobs = count => ({ type: types.REQUESTED_JOBS, count });
export const succeededJobs = profiles => ({ type: types.SUCCEEDED_JOBS, profiles });
export const failedJobs = error => ({ type: types.FAILED_JOBS, error });

export const setCurrentPage = page => ({ type: types.SET_CURRENT_JOBS_PAGE, page });
