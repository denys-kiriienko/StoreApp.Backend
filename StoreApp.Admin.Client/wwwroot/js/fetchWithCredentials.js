export async function fetchWithCredentials(url, options) {
    options = options || {};
    options.credentials = 'include';

    const response = await fetch(url, options);

    if (!response.ok) {
        throw new Error('Fetch failed: ' + response.status);
    }

    const contentType = response.headers.get('content-type');

    if (contentType && contentType.includes('application/json')) {
        return await response.json();
    } else {
        return await response.text();
    }
}
