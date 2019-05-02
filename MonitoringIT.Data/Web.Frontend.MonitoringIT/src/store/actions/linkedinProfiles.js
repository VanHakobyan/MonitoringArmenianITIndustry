import types from "store/types";

// add pagination
// export const requestAllLinkedinProfiles = () => ({ type: types.REQUESTED_ALL_LINKEDIN_PROFILES });
// export const succeededAllLinkedinProfiles = profiles => ({ type: types.SUCCEEDED_ALL_LINKEDIN_PROFILES, profiles });
// export const failedAllLinkedinProfiles = error => ({ type: types.FAILED_ALL_LINKEDIN_PROFILES, error });
// presentation data
export const requestFavoriteLinkedinProfiles = count => ({ type: types.REQUESTED_FAVORITE_LINKEDIN_PROFILES, count });
export const succeededFavoriteLinkedinProfiles = profiles => ({ type: types.SUCCEEDED_FAVORITE_LINKEDIN_PROFILES, profiles });
export const failedFavoriteLinkedinProfiles = error => ({ type: types.FAILED_FAVORITE_LINKEDIN_PROFILES, error });
