const API_URL = process.env.API_URL || 'http://localhost:5000';

/** @type {import('next').NextConfig} */
const nextConfig = {
    async rewrites() {
        return [
            {
                source: '/backendAPI/:path*',
                destination: `${API_URL}/:path*`,
            },
        ];
    }
};

export default nextConfig;
