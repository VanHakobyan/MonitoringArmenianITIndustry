import types from "store/types";

// all
 export const requestAllLinkedinProfiles = () => ({ type: types.REQUESTED_ALL_LINKEDIN_PROFILES });
 export const succeededAllLinkedinProfiles = profiles => ({ type: types.SUCCEEDED_ALL_LINKEDIN_PROFILES, profiles });
 export const failedAllLinkedinProfiles = error => ({ type: types.FAILED_ALL_LINKEDIN_PROFILES, error });
// presentation data
export const requestFavoriteLinkedinProfiles = count => ({ type: types.REQUESTED_FAVORITE_LINKEDIN_PROFILES, count });
export const succeededFavoriteLinkedinProfiles = profiles => ({ type: types.SUCCEEDED_FAVORITE_LINKEDIN_PROFILES, profiles });
export const failedFavoriteLinkedinProfiles = error => ({ type: types.FAILED_FAVORITE_LINKEDIN_PROFILES, error });
//by page
export const requestLinkedinProfilesByPage = () => ({ type: types.REQUESTED_LINKEDIN_PROFILES_BY_PAGE });
export const succeededLinkedinProfilesByPage = profiles => ({ type: types.SUCCEEDED_LINKEDIN_PROFILES_BY_PAGE, profiles });
export const failedLinkedinProfilesByPage = error => ({ type: types.FAILED_LINKEDIN_PROFILES_BY_PAGE, error });
