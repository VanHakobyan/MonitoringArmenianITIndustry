import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_ALL_GITHUB_PROFILES:
			return {
				...state,
				allProfilesLoading: true,
				allProfilesSuccess: undefined,
				allProfilesFailed: undefined
			};
		case types.SUCCEEDED_ALL_GITHUB_PROFILES:
			return {
				...state,
				allProfilesLoading: false,
				allProfilesSuccess: action.profiles,
				allProfilesFailed: undefined
			};
		case types.FAILED_ALL_GITHUB_PROFILES:
			return {
				...state,
				allProfilesLoading: false,
				allProfilesSuccess: undefined,
				allProfilesFailed: action.error
			};
		default:
			return state;
	}
}