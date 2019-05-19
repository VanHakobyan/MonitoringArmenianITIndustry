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

        case types.REQUESTED_LINKEDIN_PROFILES_BY_PAGE:
            return {
                ...state,
                byPageLinkedinProfilesLoading: true,
                byPageLinkedinProfilesSuccess: undefined,
                byPageLinkedinProfilesFailed: undefined
            };
        case types.SUCCEEDED_LINKEDIN_PROFILES_BY_PAGE:
            return {
                ...state,
                byPageLinkedinProfilesSuccess: action.profiles,
                byPageLinkedinProfilesFailed: undefined,
                byPageLinkedinProfilesLoading: true
            };
        case types.FAILED_LINKEDIN_PROFILES_BY_PAGE:
            return {
                ...state,
                byPageLinkedinProfilesLoading: true,
                byPageLinkedinProfilesSuccess: undefined,
                byPageLinkedinProfilesFailed: action.error
            };
        default:
            return state;
    }
}

