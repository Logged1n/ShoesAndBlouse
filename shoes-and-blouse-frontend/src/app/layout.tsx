import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "../styles/globals.css";

const inter = Inter({ subsets: ["latin"] });

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
      <div className="mainLayout">
          {/*HEADER COMPONENT*/}
        <main className="mainContent">
            {children}
        </main>
          {/*FOOTER COMPONENT*/}
      </div>
      </body>
    </html>
  );
}
