export const jobsLoadingSelector = state => state.jobs.jobsLoading;
export const jobsSuccessSelector = state => state.jobs.jobsSuccess;
export const jobsFailedSelector = state => state.jobs.jobsFailed;

export const currentJobsPageSelector = state => state.jobs.currentJobsPage;