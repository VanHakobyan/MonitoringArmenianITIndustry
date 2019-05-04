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
        default:
            return state;
    }
}