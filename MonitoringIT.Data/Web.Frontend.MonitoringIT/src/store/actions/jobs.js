import types from "store/types";
//by page
export const requestJobsByPage = () => ({ type: types.REQUESTED_JOBS_BY_PAGE });
export const succeededJobsByPage = profiles => ({ type: types.SUCCEEDED_JOBS_BY_PAGE, profiles });
export const failedJobsByPage = error => ({ type: types.FAILED_JOBS_BY_PAGE, error });
