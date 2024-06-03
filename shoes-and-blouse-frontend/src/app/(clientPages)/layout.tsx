import type { Metadata } from "next";
import "@/styles/globals.css";
import React from "react";
import { AppRouterCacheProvider } from '@mui/material-nextjs/v13-appRouter';
import Header from "@/components/Header/Header";

export const metadata: Metadata = {
  title: "Shoes And Blouse",
  description: "The best boots and cloths ever!",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
      <AppRouterCacheProvider options={{enableCssLayer: true}}>
        <div className="mainLayout">
            <Header />
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
