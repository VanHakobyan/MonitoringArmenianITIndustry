import types from "store/types";

export default function reduce(state = {}, action) {
	switch (action.type) {
		case types.REQUESTED_FAVORITE_LINKEDIN_PROFILES:
			return {
				...state,
				favoriteLinkedinProfilesLoading: true,
				favoriteLinkedinProfilesSuccess: undefined,
				favoriteLinkedinProfilesFailed: undefined
			};
		case types.SUCCEEDED_FAVORITE_LINKEDIN_PROFILES:
			return {
				...state,
				favoriteLinkedinProfilesLoading: true,
				favoriteLinkedinProfilesSuccess: action.profiles,
				favoriteLinkedinProfilesFailed: undefined
			};
		case types.FAILED_FAVORITE_LINKEDIN_PROFILES:
			return {
				...state,
				favoriteLinkedinProfilesLoading: true,
				favoriteLinkedinProfilesSuccess: undefined,
				favoriteLinkedinProfilesFailed: action.error
			};

		case types.REQUESTED_ALL_LINKEDIN_PROFILES:
			return {
				...state,
				linkedinProfilesLoading: true,
				linkedinProfilesFailed: undefined
			};
		case types.SUCCEEDED_ALL_LINKEDIN_PROFILES:
			return {
				...state,
				linkedinProfilesSuccess: action.profiles,
				linkedinProfilesFailed: undefined,
				linkedinProfilesLoading: true
			};
		case types.FAILED_ALL_LINKEDIN_PROFILES:
			return {
				...state,
				linkedinProfilesLoading: true,
				linkedinProfilesSuccess: undefined,
				linkedinProfilesFailed: action.error
			};
		case types.SET_CURRENT_LINKEDIN_PAGE:
			return {
				...state,
				currentLinkedinPage: action.page
			};
		default:
			return state;
	}
}