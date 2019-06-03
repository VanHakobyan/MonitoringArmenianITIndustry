import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_JOBS:
			return {
				...state,
				jobsLoading: true,
				jobsFailed: undefined
			};
		case types.SUCCEEDED_JOBS:
			return {
				...state,
				jobsLoading: true,
				jobsSuccess: action.profiles,
				jobsFailed: undefined
			};
		case types.FAILED_JOBS:
			return {
				...state,
				jobsLoading: true,
				jobsSuccess: undefined,
				jobsFailed: action.error
			};
		case types.SET_CURRENT_JOBS_PAGE:
			return {
				...state,
				currentJobsPage: action.page
			};
		default:
			return state;
	}
}