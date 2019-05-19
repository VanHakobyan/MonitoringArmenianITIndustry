import types from "store/types";

export default function reduce(state = {}, action) {
    switch (action.type) {
        case types.REQUESTED_JOBS_BY_PAGE:
            return {
                ...state,
                byPageJobsLoading: true,
                byPageJobsSuccess: undefined,
                byPageJobsFailed: undefined
            };
        case types.SUCCEEDED_JOBS_BY_PAGE:
            return {
                ...state,
                byPageJobsLoading: true,
                byPageJobsSuccess: action.profiles,
                byPageJobsFailed: undefined
            };
        case types.FAILED_JOBS_BY_PAGE:
            return {
                ...state,
                byPageJobsLoading: true,
                byPageJobsSuccess: undefined,
                byPageJobsFailed: action.error
            };
        default:
            return state;
    }
}