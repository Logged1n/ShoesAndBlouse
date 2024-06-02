"use client"

import React from "react";
import { AppRouterCacheProvider } from '@mui/material-nextjs/v13-appRouter';
import ToolBar from "@/components/ToolBar";

export default function RootLayout({children,}: { children: React.ReactNode; }) {
    return (
        <html lang="en">
        <body>
        <AppRouterCacheProvider options={{enableCssLayer: true}}>
            <ToolBar />
            <div className="mainLayout">
                <main className="mainContent">
                    {children}
                </main>
                {/*FOOTER COMPONENT*/}
            </div>
        </AppRouterCacheProvider>
        </body>
        </html>
    );
}
