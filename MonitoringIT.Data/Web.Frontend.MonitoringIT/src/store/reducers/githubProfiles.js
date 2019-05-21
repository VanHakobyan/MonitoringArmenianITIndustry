import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_ALL_GITHUB_PROFILES:
			return {
				...state,
				allProfilesLoading: true,
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
		case types.REQUESTED_FAVORITE_GITHUB_PROFILES:
			return {
				...state,
				favoriteGithubProfilesLoading: true,
				favoriteGithubProfilesFailed: undefined
			};
		case types.SUCCEEDED_FAVORITE_GITHUB_PROFILES:
			return {
				...state,
				favoriteGithubProfilesLoading: true,
				favoriteGithubProfilesSuccess: action.profiles,
				favoriteGithubProfilesFailed: undefined
			};
		case types.FAILED_FAVORITE_GITHUB_PROFILES:
			return {
				...state,
				favoriteGithubProfilesLoading: true,
				favoriteGithubProfilesSuccess: undefined,
				favoriteGithubProfilesFailed: action.error
			};
		case types.SET_CURRENT_GITHUB_PAGE:
			return {
				...state,
				currentGithubPage: action.page
			};
		default:
			return state;
	}
}